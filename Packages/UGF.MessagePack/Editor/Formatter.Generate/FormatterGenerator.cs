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
            foreach (SyntaxNode node in GetFields(info, generator))
            {
                yield return node;
            }

            yield return GetConstructor(info, generator, GetConstructorStatements(info, generator));
            yield return GetInitializeMethod(info, generator, GetInitializeMethodStatements(info, generator));
            yield return GetSerializeMethod(info, generator, GetSerializeMethodStatements(info, generator));
            yield return GetDeserializeMethod(info, generator, GetDeserializeMethodStatements(info, generator));
        }

        protected override IEnumerable<SyntaxNode> GetFields(TInfo info, SyntaxGenerator generator)
        {
            foreach (KeyValuePair<string, FormatterGenerateInfo.FormatterInfo> pair in info.FormatterInfos)
            {
                yield return generator.FieldDeclaration(pair.Key, pair.Value.FormatterType);
            }
        }

        protected override SyntaxNode GetConstructor(TInfo info, SyntaxGenerator generator, IEnumerable<SyntaxNode> statements)
        {
            SyntaxNode providerParameter = generator.ParameterDeclaration("provider", info.ProviderType);
            SyntaxNode contextParameter = generator.ParameterDeclaration("context", info.ContextType);
            SyntaxNode providerArgument = generator.Argument(generator.IdentifierName("provider"));
            SyntaxNode contextArgument = generator.Argument(generator.IdentifierName("context"));

            return generator.ConstructorDeclaration(info.Name, new[] { providerParameter, contextParameter }, Accessibility.Public, DeclarationModifiers.None, new[] { providerArgument, contextArgument }, statements);
        }

        protected override IEnumerable<SyntaxNode> GetConstructorStatements(TInfo info, SyntaxGenerator generator)
        {
            return Enumerable.Empty<SyntaxNode>();
        }

        protected override SyntaxNode GetInitializeMethod(TInfo info, SyntaxGenerator generator, IEnumerable<SyntaxNode> statements)
        {
            SyntaxNode returnType = SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword));

            return generator.MethodDeclaration("Initialize", null, null, returnType, Accessibility.Public, DeclarationModifiers.Override, statements);
        }

        protected override IEnumerable<SyntaxNode> GetInitializeMethodStatements(TInfo info, SyntaxGenerator generator)
        {
            yield return generator.InvocationExpression(generator.MemberAccessExpression(generator.BaseExpression(), "Initialize"));

            foreach (KeyValuePair<string, FormatterGenerateInfo.FormatterInfo> pair in info.FormatterInfos)
            {
                SyntaxNode access = generator.MemberAccessExpression(generator.IdentifierName("Provider"), "TryGet");
                SyntaxNode argument = generator.Argument(RefKind.Out, generator.IdentifierName(pair.Key));
                SyntaxNode condition = generator.LogicalNotExpression(generator.InvocationExpression(access, argument));
                SyntaxNode stringFormatAccess = generator.MemberAccessExpression(info.StringType, "Format");
                SyntaxNode exceptionArgument = generator.InvocationExpression(stringFormatAccess, generator.LiteralExpression(info.ArgumentExceptionText), generator.TypeOfExpression(pair.Value.TargetType));
                SyntaxNode exception = generator.ObjectCreationExpression(info.ArgumentExceptionType, exceptionArgument);
                SyntaxNode trueStatement = generator.ThrowStatement(exception);

                yield return generator.IfStatement(condition, new[] { trueStatement });
            }
        }

        protected override SyntaxNode GetSerializeMethod(TInfo info, SyntaxGenerator generator, IEnumerable<SyntaxNode> statements)
        {
            SyntaxNode returnType = SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword));
            SyntaxNode writerParameter = generator.ParameterDeclaration("writer", info.WriterType, null, RefKind.Ref);
            SyntaxNode valueParameter = generator.ParameterDeclaration("value", info.TargetType);

            return generator.MethodDeclaration("Serialize", new[] { writerParameter, valueParameter }, null, returnType, Accessibility.Public, DeclarationModifiers.Override, statements);
        }

        protected override IEnumerable<SyntaxNode> GetSerializeMethodStatements(TInfo info, SyntaxGenerator generator)
        {
            SyntaxNode condition = generator.ValueNotEqualsExpression(generator.IdentifierName("value"), generator.DefaultExpression(info.TargetType));
            IEnumerable<SyntaxNode> trueStatements = GetWriteStatements(info, generator);
            SyntaxNode access = generator.MemberAccessExpression(generator.IdentifierName("writer"), "WriteNil");
            SyntaxNode falseStatement = generator.ExpressionStatement(generator.InvocationExpression(access));

            yield return generator.IfStatement(condition, trueStatements, falseStatement);
        }

        protected override IEnumerable<SyntaxNode> GetWriteStatements(TInfo info, SyntaxGenerator generator)
        {
            return Enumerable.Empty<SyntaxNode>();
        }

        protected override SyntaxNode GetDeserializeMethod(TInfo info, SyntaxGenerator generator, IEnumerable<SyntaxNode> statements)
        {
            SyntaxNode readerParameter = generator.ParameterDeclaration("reader", info.ReaderType, null, RefKind.Ref);

            return generator.MethodDeclaration("Deserialize", new[] { readerParameter }, null, info.TargetType, Accessibility.Public, DeclarationModifiers.Override, statements);
        }

        protected override IEnumerable<SyntaxNode> GetDeserializeMethodStatements(TInfo info, SyntaxGenerator generator)
        {
            SyntaxNode condition = generator.LogicalNotExpression(generator.InvocationExpression(generator.IdentifierName("reader.TryReadNil")));
            IEnumerable<SyntaxNode> trueStatements = GetTrueStatements();

            yield return generator.IfStatement(condition, trueStatements);
            yield return generator.ReturnStatement(generator.DefaultExpression(info.TargetType));

            IEnumerable<SyntaxNode> GetTrueStatements()
            {
                yield return generator.LocalDeclarationStatement("value", generator.ObjectCreationExpression(info.TargetType));

                foreach (SyntaxNode node in GetReadStatements(info, generator))
                {
                    yield return node;
                }

                yield return generator.ReturnStatement(generator.IdentifierName("value"));
            }
        }

        protected override IEnumerable<SyntaxNode> GetReadStatements(TInfo info, SyntaxGenerator generator)
        {
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}
