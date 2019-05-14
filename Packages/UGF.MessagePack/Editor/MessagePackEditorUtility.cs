using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MessagePack;
using MessagePack.UniversalCodeGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using UGF.Assemblies.Editor;
using UGF.Code.Analysis.Editor;
using UGF.Code.Generate.Editor;
using UGF.Code.Generate.Editor.Container;
using UGF.Code.Generate.Editor.Container.External;
using UGF.MessagePack.Editor.ExternalType;
using UGF.MessagePack.Runtime;
using UnityEditor;
using UnityEditor.Compilation;

namespace UGF.MessagePack.Editor
{
    /// <summary>
    /// Provides utilities to work with MessagePack serialization in editor.
    /// </summary>
    public static class MessagePackEditorUtility
    {
        /// <summary>
        /// Generates asset with the generated code for assembly from the specified path.
        /// </summary>
        /// <param name="path">The path of the assembly definition file.</param>
        /// <param name="import">The value determines whether to force asset database import.</param>
        /// <param name="validation">The container type validation used to generate externals.</param>
        /// <param name="compilation">The project compilation used during generation.</param>
        /// <param name="generator">The syntax generator used during generation.</param>
        public static void GenerateAssetFromAssembly(string path, bool import = true, ICodeGenerateContainerValidation validation = null, Compilation compilation = null, SyntaxGenerator generator = null)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (validation == null) validation = CodeGenerateContainerExternalEditorUtility.DefaultValidation;
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            if (generator == null) generator = CodeAnalysisEditorUtility.Generator;

            string sourcePath = CodeGenerateEditorUtility.GetPathForGeneratedScript(path, "MessagePack");
            string source = GenerateFromAssembly(path, validation, compilation, generator);

            File.WriteAllText(sourcePath, source);

            if (import)
            {
                AssetDatabase.ImportAsset(sourcePath);
            }
        }

        /// <summary>
        /// Generates source of the generated code for assembly from the specified path.
        /// </summary>
        /// <param name="path">The path of the assembly definition file.</param>
        /// <param name="validation">The container type validation used to generate externals.</param>
        /// <param name="compilation">The project compilation used during generation.</param>
        /// <param name="generator">The syntax generator used during generation.</param>
        public static string GenerateFromAssembly(string path, ICodeGenerateContainerValidation validation = null, Compilation compilation = null, SyntaxGenerator generator = null)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (validation == null) validation = CodeGenerateContainerExternalEditorUtility.DefaultValidation;
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            if (generator == null) generator = CodeAnalysisEditorUtility.Generator;

            string assemblyName = Path.GetFileNameWithoutExtension(path);

            if (!AssemblyEditorUtility.TryFindCompilationAssemblyByName(assemblyName, out Assembly assembly))
            {
                throw new ArgumentException($"Assembly not found from the specified path: '{path}'.");
            }

            var sourcePaths = new List<string>();

            for (int i = 0; i < assembly.sourceFiles.Length; i++)
            {
                string sourcePath = assembly.sourceFiles[i];

                if (CodeGenerateEditorUtility.CheckAttributeFromScript(compilation, sourcePath, typeof(MessagePackObjectAttribute)))
                {
                    sourcePaths.Add(sourcePath);
                }
            }

            var externals = new List<string>();
            string externalsTempPath = string.Empty;

            AssemblyEditorUtility.GetAssetPathsUnderAssemblyDefinitionFile(externals, path, MessagePackExternalTypeEditorUtility.ExternalTypeAssetExtensionName);

            if (externals.Count > 0)
            {
                externalsTempPath = FileUtil.GetUniqueTempPathInProject();

                Directory.CreateDirectory(externalsTempPath);

                for (int i = 0; i < externals.Count; i++)
                {
                    string externalPath = externals[i];

                    if (CodeGenerateContainerExternalEditorUtility.TryGetInfoFromAssetPath(externalPath, out MessagePackExternalTypeInfo info) && info.TryGetTargetType(out _))
                    {
                        SyntaxNode unit = MessagePackExternalTypeEditorUtility.CreateUnit(info, validation, compilation, generator);

                        string sourcePath = $"{externalsTempPath}/{Guid.NewGuid():N}.cs";
                        string source = unit.NormalizeWhitespace().ToFullString();

                        File.WriteAllText(sourcePath, source);

                        sourcePaths.Add(sourcePath);
                    }
                }
            }

            string formatters = GenerateFormatters(sourcePaths, assembly.name, compilation, generator);

            if (!string.IsNullOrEmpty(externalsTempPath))
            {
                FileUtil.DeleteFileOrDirectory(externalsTempPath);
            }

            return formatters;
        }

        /// <summary>
        /// Generates source of the formatters from the specified path of the sources.
        /// </summary>
        /// <param name="sourcePaths">The collection of the source paths.</param>
        /// <param name="namespaceRoot">The namespace root of the generated formatters.</param>
        /// <param name="compilation">The project compilation used during generation.</param>
        /// <param name="generator">The syntax generator used during generation.</param>
        public static string GenerateFormatters(IReadOnlyList<string> sourcePaths, string namespaceRoot, Compilation compilation = null, SyntaxGenerator generator = null)
        {
            if (sourcePaths == null) throw new ArgumentNullException(nameof(sourcePaths));
            if (namespaceRoot == null) throw new ArgumentNullException(nameof(namespaceRoot));
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            if (generator == null) generator = CodeAnalysisEditorUtility.Generator;

            if (!compilation.TryConstructTypeSymbol(typeof(MessagePackFormatterResolverAttribute), out INamedTypeSymbol attributeTypeSymbol))
            {
                throw new ArgumentException($"The '{typeof(MessagePackFormatterResolverAttribute).FullName}' type symbol not found in specified compilation.", nameof(compilation));
            }

            var arguments = new MessagePackGenerateArguments
            {
                IgnoreReadOnly = true,
                IgnoreNotMarked = true,
                GenerateEnumFormatters = true,
                GenerateUnionFormatters = true
            };

            var walkerCollectUsings = new CodeGenerateWalkerCollectUsingDirectives();

            var rewriterAddAttribute = new CodeGenerateRewriterAddAttributeToNode(generator, generator.Attribute(generator.TypeExpression(attributeTypeSymbol)), declaration =>
            {
                SyntaxKind kind = declaration.Kind();

                if (kind == SyntaxKind.ClassDeclaration)
                {
                    var classDeclarationSyntax = (ClassDeclarationSyntax)declaration;

                    if (classDeclarationSyntax.BaseList?.Types.Count > 0)
                    {
                        TypeSyntax typeSyntax = classDeclarationSyntax.BaseList.Types[0].Type;

                        if (typeSyntax is NameSyntax nameSyntax)
                        {
                            if (nameSyntax is QualifiedNameSyntax qualifiedNameSyntax)
                            {
                                nameSyntax = qualifiedNameSyntax.Right;
                            }

                            if (nameSyntax is SimpleNameSyntax simpleNameSyntax && simpleNameSyntax.Identifier.Text == typeof(IFormatterResolver).Name)
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            });

            var rewriterFormatAttribute = new CodeGenerateRewriterFormatAttributeList();

            for (int i = 0; i < sourcePaths.Count; i++)
            {
                walkerCollectUsings.Visit(SyntaxFactory.ParseSyntaxTree(File.ReadAllText(sourcePaths[i])).GetRoot());
            }

            string formatters = MessagePackUniversalCodeGeneratorUtility.Generate(sourcePaths, "ResolverGenerated", namespaceRoot, arguments);
            CompilationUnitSyntax unit = SyntaxFactory.ParseCompilationUnit(formatters);

            unit = unit.AddUsings(walkerCollectUsings.UsingDirectives.Select(x => x.WithoutLeadingTrivia()).ToArray());
            unit = (CompilationUnitSyntax)rewriterAddAttribute.Visit(unit);
            unit = (CompilationUnitSyntax)rewriterFormatAttribute.Visit(unit);
            unit = CodeGenerateEditorUtility.AddGeneratedCodeLeadingTrivia(unit);

            return unit.ToFullString();
        }
    }
}
