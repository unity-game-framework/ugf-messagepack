using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace UGF.MessagePack.Editor.Formatter.Generate
{
    public abstract class FormatterGeneratorBase<TInfo> where TInfo : FormatterGenerateInfo
    {
        public Compilation Compilation { get; }
        public SyntaxGenerator Generator { get; }

        protected FormatterGeneratorBase(Compilation compilation, SyntaxGenerator generator)
        {
            Compilation = compilation;
            Generator = generator;
        }

        public abstract SyntaxNode Generate(TInfo info);
        protected abstract SyntaxNode GetNamespace(TInfo info);
        protected abstract SyntaxNode GetClass(TInfo info, IEnumerable<SyntaxNode> members);
        protected abstract IEnumerable<SyntaxNode> GetClassMembers(TInfo info);
        protected abstract IEnumerable<SyntaxNode> GetFields(TInfo info);
        protected abstract SyntaxNode GetConstructor(TInfo info, IEnumerable<SyntaxNode> statements);
        protected abstract IEnumerable<SyntaxNode> GetConstructorStatements(TInfo info);
        protected abstract SyntaxNode GetInitializeMethod(TInfo info, IEnumerable<SyntaxNode> statements);
        protected abstract IEnumerable<SyntaxNode> GetInitializeMethodStatements(TInfo info);
        protected abstract SyntaxNode GetSerializeMethod(TInfo info, IEnumerable<SyntaxNode> statements);
        protected abstract IEnumerable<SyntaxNode> GetSerializeMethodStatements(TInfo info);
        protected abstract IEnumerable<SyntaxNode> GetWriteStatements(TInfo info);
        protected abstract SyntaxNode GetDeserializeMethod(TInfo info, IEnumerable<SyntaxNode> statements);
        protected abstract IEnumerable<SyntaxNode> GetDeserializeMethodStatements(TInfo info);
        protected abstract IEnumerable<SyntaxNode> GetReadStatements(TInfo info);
    }
}
