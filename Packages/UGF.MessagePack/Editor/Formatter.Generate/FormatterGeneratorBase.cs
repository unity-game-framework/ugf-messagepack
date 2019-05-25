using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace UGF.MessagePack.Editor.Formatter.Generate
{
    public abstract class FormatterGeneratorBase<TInfo> where TInfo : FormatterGenerateInfo
    {
        public abstract SyntaxNode Generate(TInfo info, SyntaxGenerator generator);
        protected abstract SyntaxNode GetNamespace(TInfo info, SyntaxGenerator generator);
        protected abstract SyntaxNode GetClass(TInfo info, SyntaxGenerator generator, IEnumerable<SyntaxNode> members);
        protected abstract IEnumerable<SyntaxNode> GetClassMembers(TInfo info, SyntaxGenerator generator);
        protected abstract IEnumerable<SyntaxNode> GetFields(TInfo info, SyntaxGenerator generator);
        protected abstract SyntaxNode GetConstructor(TInfo info, SyntaxGenerator generator, IEnumerable<SyntaxNode> statements);
        protected abstract IEnumerable<SyntaxNode> GetConstructorStatements(TInfo info, SyntaxGenerator generator);
        protected abstract SyntaxNode GetInitializeMethod(TInfo info, SyntaxGenerator generator, IEnumerable<SyntaxNode> statements);
        protected abstract IEnumerable<SyntaxNode> GetInitializeMethodStatements(TInfo info, SyntaxGenerator generator);
        protected abstract SyntaxNode GetSerializeMethod(TInfo info, SyntaxGenerator generator, IEnumerable<SyntaxNode> statements);
        protected abstract IEnumerable<SyntaxNode> GetSerializeMethodStatements(TInfo info, SyntaxGenerator generator);
        protected abstract IEnumerable<SyntaxNode> GetWriteStatements(TInfo info, SyntaxGenerator generator);
        protected abstract SyntaxNode GetDeserializeMethod(TInfo info, SyntaxGenerator generator, IEnumerable<SyntaxNode> statements);
        protected abstract IEnumerable<SyntaxNode> GetDeserializeMethodStatements(TInfo info, SyntaxGenerator generator);
        protected abstract IEnumerable<SyntaxNode> GetReadStatements(TInfo info, SyntaxGenerator generator);
    }
}
