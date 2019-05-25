using Microsoft.CodeAnalysis;
using NUnit.Framework;
using UGF.Code.Analysis.Editor;
using UGF.MessagePack.Editor.Formatter.Generate;

namespace UGF.MessagePack.Editor.Tests.Formatter.Generate
{
    public class TestFormatterGenerator
    {
        [Test]
        public void Generate()
        {
            FormatterGenerateInfo info = FormatterGenerateUtility.CreateDefaultInfo(typeof(bool));
            var generator = new FormatterGenerator<FormatterGenerateInfo>();
            SyntaxNode syntaxNode = generator.Generate(info, CodeAnalysisEditorUtility.Generator);

            Assert.Pass(syntaxNode.NormalizeWhitespace().ToFullString());
        }
    }
}
