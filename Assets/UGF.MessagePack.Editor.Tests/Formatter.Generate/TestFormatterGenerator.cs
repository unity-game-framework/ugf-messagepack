using System;
using Microsoft.CodeAnalysis;
using NUnit.Framework;
using UGF.Code.Analysis.Editor;
using UGF.Code.Generate.Editor;
using UGF.MessagePack.Editor.Formatter.Generate;
using UGF.MessagePack.Runtime;

namespace UGF.MessagePack.Editor.Tests.Formatter.Generate
{
    public class TestFormatterGenerator
    {
        [Test]
        public void Generate()
        {
            FormatterGenerateInfo info = FormatterGenerateUtility.CreateDefaultInfo(typeof(bool));

            CodeAnalysisEditorUtility.ProjectCompilation.TryConstructTypeSymbol(typeof(TypeCode), out INamedTypeSymbol typeCodeTypeSymbol);
            CodeAnalysisEditorUtility.ProjectCompilation.TryConstructTypeSymbol(typeof(IMessagePackFormatter<>).MakeGenericType(typeof(TypeCode)), out INamedTypeSymbol formatterTypeSymbol);

            SyntaxNode typeCodeType = CodeAnalysisEditorUtility.Generator.TypeExpression(typeCodeTypeSymbol);
            SyntaxNode formatterType = CodeAnalysisEditorUtility.Generator.TypeExpression(formatterTypeSymbol);

            info.FormatterInfos.Add("m_formatterTypeCode", new FormatterGenerateInfo.FormatterInfo
            {
                FormatterType = formatterType,
                TargetType = typeCodeType
            });

            var generator = new FormatterGenerator<FormatterGenerateInfo>();

            SyntaxNode syntaxNode = generator.Generate(info, CodeAnalysisEditorUtility.Generator);

            Assert.Pass(syntaxNode.NormalizeWhitespace().ToFullString());
        }
    }
}
