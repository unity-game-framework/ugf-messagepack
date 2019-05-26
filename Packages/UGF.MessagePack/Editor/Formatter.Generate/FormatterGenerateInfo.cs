using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace UGF.MessagePack.Editor.Formatter.Generate
{
    public class FormatterGenerateInfo
    {
        public string Namespace { get; set; }
        public string Name { get; set; }
        public SyntaxNode BaseType { get; set; }
        public SyntaxNode TargetType { get; set; }
        public SyntaxNode ProviderType { get; set; }
        public SyntaxNode ContextType { get; set; }
        public SyntaxNode WriterType { get; set; }
        public SyntaxNode ReaderType { get; set; }
        public Dictionary<string, FormatterInfo> FormatterInfos { get; } = new Dictionary<string, FormatterInfo>();
        public SyntaxNode ArgumentExceptionType { get; set; }
        public string ArgumentExceptionText { get; set; } = "The formatter for the specified type not found: '{0}'.";
        public SyntaxNode StringType { get; set; }

        public class FormatterInfo
        {
            public SyntaxNode FormatterType { get; set; }
            public SyntaxNode TargetType { get; set; }
        }
    }
}
