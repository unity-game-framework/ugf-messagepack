using System;
using MessagePack;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;
using UGF.Code.Analysis.Editor;
using UGF.Code.Generate.Editor;
using UGF.Code.Generate.Editor.Container;
using UGF.Code.Generate.Editor.Container.External;
using UGF.MessagePack.Editor.ExternalType.Analysis;

namespace UGF.MessagePack.Editor.ExternalType
{
    public static class MessagePackExternalTypeEditorUtility
    {
        public static string ExternalTypeAssetExtensionName { get; } = "messagepack-external";

        public static SyntaxNode CreateUnit(MessagePackExternalTypeInfo info, ICodeGenerateContainerValidation validation = null, Compilation compilation = null, SyntaxGenerator generator = null)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            if (validation == null) validation = CodeGenerateContainerExternalEditorUtility.DefaultValidation;
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            if (generator == null) generator = CodeAnalysisEditorUtility.Generator;

            if (!compilation.TryConstructTypeSymbol(typeof(MessagePackObjectAttribute), out INamedTypeSymbol objectAttributeTypeSymbol))
            {
                throw new ArgumentException($"The '{typeof(MessagePackObjectAttribute).FullName}' type symbol not found in specified compilation.", nameof(compilation));
            }

            if (!compilation.TryConstructTypeSymbol(typeof(KeyAttribute), out INamedTypeSymbol keyAttributeTypeSymbol))
            {
                throw new ArgumentException($"The '{typeof(KeyAttribute).FullName}' type symbol not found in specified compilation.", nameof(compilation));
            }

            SyntaxNode unit = CodeGenerateContainerExternalEditorUtility.CreateUnit(info, validation, compilation, generator);

            SyntaxNode objectAttribute = generator.Attribute(generator.TypeExpression(objectAttributeTypeSymbol), new[]
            {
                generator.LiteralExpression(info.KeyAsPropertyName)
            });

            SyntaxNode keyAttributeType = generator.TypeExpression(keyAttributeTypeSymbol);

            var rewriterAddObjectAttribute = new CodeGenerateRewriterAddAttributeToNode(generator, objectAttribute, declaration =>
            {
                SyntaxKind kind = declaration.Kind();

                return kind == SyntaxKind.ClassDeclaration || kind == SyntaxKind.StructDeclaration;
            });

            var rewriterAddKeyAttribute = new MessagePackExternalTypeRewriterAddKeyAttribute(generator, keyAttributeType, info);

            unit = rewriterAddObjectAttribute.Visit(unit);
            unit = rewriterAddKeyAttribute.Visit(unit);

            return unit;
        }
    }
}
