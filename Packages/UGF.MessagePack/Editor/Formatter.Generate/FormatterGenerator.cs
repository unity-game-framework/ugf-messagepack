using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;

namespace UGF.MessagePack.Editor.Formatter.Generate
{
    public class FormatterGenerator<TInfo> : FormatterGeneratorBase<TInfo> where TInfo : FormatterGenerateInfo
    {
        public override SyntaxNode Generate(TInfo info, SyntaxGenerator generator)
        {
            return generator.CompilationUnit(GetNamespace(info, generator));
        }

        protected override SyntaxNode GetNamespace(TInfo info, SyntaxGenerator generator)
        {
            return generator.NamespaceDeclaration(info.Namespace, GetClass(info, generator, GetClassMembers(info, generator)));
        }

        protected override SyntaxNode GetClass(TInfo info, SyntaxGenerator generator, IEnumerable<SyntaxNode> members)
        {
            return generator.ClassDeclaration(info.Name, null, Accessibility.Public, DeclarationModifiers.None, info.BaseType, null, members);
        }

        protected override IEnumerable<SyntaxNode> GetClassMembers(TInfo info, SyntaxGenerator generator)
        {
            foreach (SyntaxNode node in GetReadonlyFields(info, generator))
            {
                yield return node;
            }

            foreach (SyntaxNode node in GetAdditionalFields(info, generator))
            {
                yield return node;
            }

            yield return GetConstructor(info, generator, GetConstructorInstructions(info, generator));
            yield return GetInitializeMethod(info, generator, GetInitializeMethodInstructions(info, generator));
            yield return GetSerializeMethod(info, generator, GetSerializeMethodInstructions(info, generator));
            yield return GetDeserializeMethod(info, generator, GetDeserializeMethodInstructions(info, generator));
        }

        protected override IEnumerable<SyntaxNode> GetReadonlyFields(TInfo info, SyntaxGenerator generator)
        {
            return Enumerable.Empty<SyntaxNode>();
        }

        protected override IEnumerable<SyntaxNode> GetAdditionalFields(TInfo info, SyntaxGenerator generator)
        {
            return Enumerable.Empty<SyntaxNode>();
        }

        protected override SyntaxNode GetConstructor(TInfo info, SyntaxGenerator generator, IEnumerable<SyntaxNode> instructions)
        {
            SyntaxNode providerParameter = generator.ParameterDeclaration("provider", info.ProviderType);
            SyntaxNode contextParameter = generator.ParameterDeclaration("context", info.ContextType);
            SyntaxNode providerArgument = generator.Argument(generator.IdentifierName("provider"));
            SyntaxNode contextArgument = generator.Argument(generator.IdentifierName("context"));

            return generator.ConstructorDeclaration(info.Name, new[] { providerParameter, contextParameter }, Accessibility.Public, DeclarationModifiers.None, new[] { providerArgument, contextArgument }, instructions);
        }

        protected override IEnumerable<SyntaxNode> GetConstructorInstructions(TInfo info, SyntaxGenerator generator)
        {
            return Enumerable.Empty<SyntaxNode>();
        }

        protected override SyntaxNode GetInitializeMethod(TInfo info, SyntaxGenerator generator, IEnumerable<SyntaxNode> instructions)
        {
            SyntaxNode returnType = SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword));

            return generator.MethodDeclaration("Initialize", null, null, returnType, Accessibility.Public, DeclarationModifiers.Override, instructions);
        }

        protected override IEnumerable<SyntaxNode> GetInitializeMethodInstructions(TInfo info, SyntaxGenerator generator)
        {
            yield return generator.InvocationExpression(generator.MemberAccessExpression(generator.BaseExpression(), "Initialize"));
        }

        protected override SyntaxNode GetSerializeMethod(TInfo info, SyntaxGenerator generator, IEnumerable<SyntaxNode> instructions)
        {
            SyntaxNode returnType = SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword));
            SyntaxNode writerParameter = generator.ParameterDeclaration("writer", info.WriterType, null, RefKind.Ref);
            SyntaxNode valueParameter = generator.ParameterDeclaration("value", info.TargetType);

            return generator.MethodDeclaration("Serialize", new[] { writerParameter, valueParameter }, null, returnType, Accessibility.Public, DeclarationModifiers.Override, instructions);
        }

        protected override IEnumerable<SyntaxNode> GetSerializeMethodInstructions(TInfo info, SyntaxGenerator generator)
        {
            return Enumerable.Empty<SyntaxNode>();
        }

        protected override SyntaxNode GetDeserializeMethod(TInfo info, SyntaxGenerator generator, IEnumerable<SyntaxNode> instructions)
        {
            SyntaxNode readerParameter = generator.ParameterDeclaration("reader", info.ReaderType, null, RefKind.Ref);

            return generator.MethodDeclaration("Deserialize", new[] { readerParameter }, null, info.TargetType, Accessibility.Public, DeclarationModifiers.Override, instructions);
        }

        protected override IEnumerable<SyntaxNode> GetDeserializeMethodInstructions(TInfo info, SyntaxGenerator generator)
        {
            yield return generator.ReturnStatement(generator.ObjectCreationExpression(info.TargetType));
        }
    }
}
