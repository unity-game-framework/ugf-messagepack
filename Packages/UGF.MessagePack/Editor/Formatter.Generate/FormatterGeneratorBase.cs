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
        protected abstract IEnumerable<SyntaxNode> GetReadonlyFields(TInfo info, SyntaxGenerator generator);
        protected abstract IEnumerable<SyntaxNode> GetAdditionalFields(TInfo info, SyntaxGenerator generator);
        protected abstract SyntaxNode GetConstructor(TInfo info, SyntaxGenerator generator, IEnumerable<SyntaxNode> instructions);
        protected abstract IEnumerable<SyntaxNode> GetConstructorInstructions(TInfo info, SyntaxGenerator generator);
        protected abstract SyntaxNode GetInitializeMethod(TInfo info, SyntaxGenerator generator, IEnumerable<SyntaxNode> instructions);
        protected abstract IEnumerable<SyntaxNode> GetInitializeMethodInstructions(TInfo info, SyntaxGenerator generator);
        protected abstract SyntaxNode GetSerializeMethod(TInfo info, SyntaxGenerator generator, IEnumerable<SyntaxNode> instructions);
        protected abstract IEnumerable<SyntaxNode> GetSerializeMethodInstructions(TInfo info, SyntaxGenerator generator);
        protected abstract SyntaxNode GetDeserializeMethod(TInfo info, SyntaxGenerator generator, IEnumerable<SyntaxNode> instructions);
        protected abstract IEnumerable<SyntaxNode> GetDeserializeMethodInstructions(TInfo info, SyntaxGenerator generator);
    }
}
