using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;
using NUnit.Framework;
using UGF.Code.Analysis.Editor;
using UGF.Code.Generate.Editor;
using UGF.MessagePack.Editor.Formatter.Generate;

namespace UGF.MessagePack.Editor.Tests.Formatter.Generate
{
    public class TestFormatterGenerator
    {
        public class Target
        {
        }

        [Test]
        public void Generate()
        {
            CSharpCompilation compilation = CodeAnalysisEditorUtility.ProjectCompilation;
            SyntaxGenerator generator = CodeAnalysisEditorUtility.Generator;

            var formatterGenerator = new FormatterGenerator<FormatterGenerateInfo>(compilation, generator);

            SyntaxNode targetType = generator.TypeExpression(compilation.ConstructTypeSymbol(typeof(Target)));
            var info = new FormatterGenerateInfo("MessagePackFormatterTarget", targetType);

            info.InitializeFormatterTypes.Add("TypeCode", generator.TypeExpression(compilation.ConstructTypeSymbol(typeof(TypeCode))));

            SyntaxNode node = formatterGenerator.Generate(info);

            Assert.Pass(node.NormalizeWhitespace().ToFullString());
        }
    }
}
