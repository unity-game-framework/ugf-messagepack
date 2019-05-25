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
    }
}
