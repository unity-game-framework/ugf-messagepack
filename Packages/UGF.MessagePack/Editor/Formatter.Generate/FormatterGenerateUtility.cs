using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using UGF.Code.Analysis.Editor;
using UGF.Code.Generate.Editor;
using UGF.MessagePack.Runtime;

namespace UGF.MessagePack.Editor.Formatter.Generate
{
    public static class FormatterGenerateUtility
    {
        public static FormatterGenerateInfo CreateDefaultInfo(Type targetType, Compilation compilation = null, SyntaxGenerator generator = null)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (compilation == null) compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            if (generator == null) generator = CodeAnalysisEditorUtility.Generator;

            string namespaceRoot = "UGF.MessagePack.Generated";
            string name = $"MessagePackFormatter{targetType.Name}";
            Type baseType = typeof(MessagePackFormatterBase<>).MakeGenericType(targetType);
            Type providerType = typeof(IMessagePackProvider);
            Type contextType = typeof(IMessagePackContext);
            Type writerType = typeof(MessagePackWriter);
            Type readerType = typeof(MessagePackReader);

            if (!compilation.TryConstructTypeSymbol(baseType, out INamedTypeSymbol baseTypeTypeSymbol))
            {
                throw new Exception();
            }

            if (!compilation.TryConstructTypeSymbol(targetType, out INamedTypeSymbol targetTypeTypeSymbol))
            {
                throw new Exception();
            }

            if (!compilation.TryConstructTypeSymbol(providerType, out INamedTypeSymbol providerTypeSymbol))
            {
                throw new Exception();
            }

            if (!compilation.TryConstructTypeSymbol(contextType, out INamedTypeSymbol contextTypeSymbol))
            {
                throw new Exception();
            }

            if (!compilation.TryConstructTypeSymbol(writerType, out INamedTypeSymbol writerTypeSymbol))
            {
                throw new Exception();
            }

            if (!compilation.TryConstructTypeSymbol(readerType, out INamedTypeSymbol readerTypeSymbol))
            {
                throw new Exception();
            }

            return new FormatterGenerateInfo
            {
                Namespace = namespaceRoot,
                Name = name,
                BaseType = generator.TypeExpression(baseTypeTypeSymbol),
                TargetType = generator.TypeExpression(targetTypeTypeSymbol),
                ProviderType = generator.TypeExpression(providerTypeSymbol),
                ContextType = generator.TypeExpression(contextTypeSymbol),
                WriterType = generator.TypeExpression(writerTypeSymbol),
                ReaderType = generator.TypeExpression(readerTypeSymbol)
            };
        }
    }
}
