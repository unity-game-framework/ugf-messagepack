using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack.UniversalCodeGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using UGF.Assemblies.Editor;
using UGF.Code.Analysis.Editor;
using UGF.Code.Generate.Editor;
using UGF.MessagePack.Editor.Analysis;
using UGF.MessagePack.Runtime;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

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
        /// <param name="compilation">The project compilation used during generation.</param>
        /// <param name="generator">The syntax generator used during generation.</param>
        public static void GenerateAssetFromAssembly(string path, bool import = true, CSharpCompilation compilation = null, SyntaxGenerator generator = null)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            if (generator == null) generator = CodeAnalysisEditorUtility.Generator;

            string sourcePath = GetPathForGeneratedScript(path);
            string source = GenerateFromAssembly(path, compilation, generator);

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
        /// <param name="compilation">The project compilation used during generation.</param>
        /// <param name="generator">The syntax generator used during generation.</param>
        public static string GenerateFromAssembly(string path, CSharpCompilation compilation = null, SyntaxGenerator generator = null)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
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

                if (IsSerializableScript(sourcePath))
                {
                    sourcePaths.Add(sourcePath);
                }
            }

            // var externals = new List<string>();
            string externalsTempPath = string.Empty;

            // AssemblyEditorUtility.GetAssetPathsUnderAssemblyDefinitionFile(externals, path, Utf8JsonExternalTypeEditorUtility.ExternalTypeAssetExtension);
            //
            // if (externals.Count > 0)
            // {
            //     externalsTempPath = FileUtil.GetUniqueTempPathInProject();
            //
            //     Directory.CreateDirectory(externalsTempPath);
            //
            //     Utf8JsonExternalTypeEditorUtility.GenerateExternalContainers(externalsTempPath, externals, sourcePaths, compilation, generator);
            // }

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
        public static string GenerateFormatters(IReadOnlyList<string> sourcePaths, string namespaceRoot, CSharpCompilation compilation = null, SyntaxGenerator generator = null)
        {
            if (sourcePaths == null) throw new ArgumentNullException(nameof(sourcePaths));
            if (namespaceRoot == null) throw new ArgumentNullException(nameof(namespaceRoot));
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            if (generator == null) generator = CodeAnalysisEditorUtility.Generator;

            var arguments = new MessagePackGenerateArguments
            {
                IgnoreReadOnly = true,
                IsTypeRequireAttribute = true,
                TypeRequiredAttributeShortName = "MessagePackSerializable",
                Compilation = compilation
            };

            INamedTypeSymbol attributeTypeSymbol = compilation.GetTypeByMetadataName(typeof(MessagePackFormatterAttribute).FullName);
            var attributeType = (TypeSyntax)generator.TypeExpression(attributeTypeSymbol);

            var walkerCollectUsings = new CodeGenerateWalkerCollectUsingDirectives();
            var rewriterAddAttribute = new MessagePackRewriterAddFormatterAttribute(generator, attributeType);
            var rewriterFormatAttribute = new CodeGenerateRewriterFormatAttributeList();

            for (int i = 0; i < sourcePaths.Count; i++)
            {
                walkerCollectUsings.Visit(SyntaxFactory.ParseSyntaxTree(File.ReadAllText(sourcePaths[i])).GetRoot());
            }

            string formatters = MessagePackUniversalCodeGeneratorUtility.GenerateFormatters(sourcePaths, namespaceRoot, arguments);
            CompilationUnitSyntax unit = SyntaxFactory.ParseCompilationUnit(formatters);

            unit = unit.AddUsings(walkerCollectUsings.UsingDirectives.Select(x => x.WithoutLeadingTrivia()).ToArray());
            unit = (CompilationUnitSyntax)rewriterAddAttribute.Visit(unit);
            unit = (CompilationUnitSyntax)rewriterFormatAttribute.Visit(unit);
            unit = CodeGenerateEditorUtility.AddGeneratedCodeLeadingTrivia(unit);

            return unit.ToFullString();
        }

        /// <summary>
        /// Gets path for generated source from the specified path.
        /// </summary>
        /// <param name="path">The path used to generated.</param>
        public static string GetPathForGeneratedScript(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

            var builder = new StringBuilder();
            string directory = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);

            if (!string.IsNullOrEmpty(directory))
            {
                directory = directory.Replace("\\", "/");

                builder.Append(directory);
                builder.Append("/");
            }

            builder.Append(name);
            builder.Append(".MessagePack.Generated.cs");

            return builder.ToString();
        }

        /// <summary>
        /// Determines whether source from the specified path contains any declaration with the <see cref="MessagePackSerializableAttribute"/> attribute.
        /// </summary>
        /// <param name="path">The path of the source.</param>
        /// <param name="compilation">The project compilation used during generation.</param>
        public static bool IsSerializableScript(string path, CSharpCompilation compilation = null)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;

            return CodeGenerateEditorUtility.CheckAttributeFromScript(compilation, path, typeof(MessagePackSerializableAttribute));
        }
    }
}
