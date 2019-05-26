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
            Type argumentExceptionType = typeof(ArgumentException);
            Type stringType = typeof(string);

            if (!compilation.TryConstructTypeSymbol(baseType, out INamedTypeSymbol baseTypeSymbol))
            {
                throw new ArgumentException($"The type symbol for the specified type not found: '{baseType}'.");
            }

            if (!compilation.TryConstructTypeSymbol(targetType, out INamedTypeSymbol targetTypeSymbol))
            {
                throw new ArgumentException($"The type symbol for the specified type not found: '{targetType}'.");
            }

            if (!compilation.TryConstructTypeSymbol(providerType, out INamedTypeSymbol providerTypeSymbol))
            {
                throw new ArgumentException($"The type symbol for the specified type not found: '{providerType}'.");
            }

            if (!compilation.TryConstructTypeSymbol(contextType, out INamedTypeSymbol contextTypeSymbol))
            {
                throw new ArgumentException($"The type symbol for the specified type not found: '{contextType}'.");
            }

            if (!compilation.TryConstructTypeSymbol(writerType, out INamedTypeSymbol writerTypeSymbol))
            {
                throw new ArgumentException($"The type symbol for the specified type not found: '{writerType}'.");
            }

            if (!compilation.TryConstructTypeSymbol(readerType, out INamedTypeSymbol readerTypeSymbol))
            {
                throw new ArgumentException($"The type symbol for the specified type not found: '{readerType}'.");
            }

            if (!compilation.TryConstructTypeSymbol(argumentExceptionType, out INamedTypeSymbol argumentExceptionTypeSymbol))
            {
                throw new ArgumentException($"The type symbol for the specified type not found: '{argumentExceptionType}'.");
            }

            if (!compilation.TryConstructTypeSymbol(stringType, out INamedTypeSymbol stringTypeSymbol))
            {
                throw new ArgumentException($"The type symbol for the specified type not found: '{stringType}'.");
            }

            return new FormatterGenerateInfo
            {
                Namespace = namespaceRoot,
                Name = name,
                BaseType = generator.TypeExpression(baseTypeSymbol),
                TargetType = generator.TypeExpression(targetTypeSymbol),
                ProviderType = generator.TypeExpression(providerTypeSymbol),
                ContextType = generator.TypeExpression(contextTypeSymbol),
                WriterType = generator.TypeExpression(writerTypeSymbol),
                ReaderType = generator.TypeExpression(readerTypeSymbol),
                ArgumentExceptionType = generator.TypeExpression(argumentExceptionTypeSymbol),
                StringType = generator.TypeExpression(stringTypeSymbol)
            };
        }
    }
}
