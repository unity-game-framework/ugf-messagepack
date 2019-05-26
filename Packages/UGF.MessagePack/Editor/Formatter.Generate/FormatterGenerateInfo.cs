using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace UGF.MessagePack.Editor.Formatter.Generate
{
    public class FormatterGenerateInfo
    {
        public string Namespace { get; set; } = "UGF.MessagePack.Generated";
        public string Name { get; set; }
        public SyntaxNode TargetType { get; set; }
        public Dictionary<string, SyntaxNode> InitializeFormatterTypes { get; } = new Dictionary<string, SyntaxNode>();

        public FormatterGenerateInfo(string name, SyntaxNode targetType)
        {
            Name = name;
            TargetType = targetType;
        }
    }
}
