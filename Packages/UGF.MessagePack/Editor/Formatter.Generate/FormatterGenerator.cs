using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;
using UGF.Code.Generate.Editor;
using UGF.MessagePack.Runtime;

namespace UGF.MessagePack.Editor.Formatter.Generate
{
    public class FormatterGenerator<TInfo> : FormatterGeneratorBase<TInfo> where TInfo : FormatterGenerateInfo
    {
        protected SyntaxNode ProviderType { get; }
        protected SyntaxNode ContextType { get; }
        protected SyntaxNode WriterType { get; }
        protected SyntaxNode ReaderType { get; }
        protected SyntaxNode ArgumentExceptionType { get; }
        protected SyntaxNode StringType { get; }
        protected SyntaxNode VoidType { get; }
        protected string ArgumentExceptionText { get; } = "The formatter for the specified type not found: '{0}'.";
        protected string BaseTypeGenericIdentifier { get; }
        protected string FormatterInterfaceTypeGenericIdentifier { get; }

        public FormatterGenerator(Compilation compilation, SyntaxGenerator generator) : base(compilation, generator)
        {
            ProviderType = generator.TypeExpression(compilation.ConstructTypeSymbol(typeof(IMessagePackProvider)));
            ContextType = generator.TypeExpression(compilation.ConstructTypeSymbol(typeof(IMessagePackContext)));
            WriterType = generator.TypeExpression(compilation.ConstructTypeSymbol(typeof(MessagePackWriter)));
            ReaderType = generator.TypeExpression(compilation.ConstructTypeSymbol(typeof(MessagePackReader)));
            ArgumentExceptionType = generator.TypeExpression(compilation.ConstructTypeSymbol(typeof(ArgumentException)));
            StringType = generator.TypeExpression(compilation.ConstructTypeSymbol(typeof(string)));
            VoidType = SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword));

            INamedTypeSymbol baseTypeSymbol = compilation.ConstructTypeSymbol(typeof(MessagePackFormatterBase<>));
            INamedTypeSymbol formatterInterfaceTypeSymbol = compilation.ConstructTypeSymbol(typeof(IMessagePackFormatter<>));

            BaseTypeGenericIdentifier = baseTypeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Replace("<T>", string.Empty);
            FormatterInterfaceTypeGenericIdentifier = formatterInterfaceTypeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Replace("<T>", string.Empty);
        }

        public override SyntaxNode Generate(TInfo info)
        {
            return Generator.CompilationUnit(GetNamespace(info));
        }

        protected override SyntaxNode GetNamespace(TInfo info)
        {
            return Generator.NamespaceDeclaration(info.Namespace, GetClass(info, GetClassMembers(info)));
        }

        protected override SyntaxNode GetClass(TInfo info, IEnumerable<SyntaxNode> members)
        {
            SyntaxNode baseType = Generator.GenericName(BaseTypeGenericIdentifier, info.TargetType);

            return Generator.ClassDeclaration(info.Name, null, Accessibility.Public, DeclarationModifiers.None, baseType, null, members);
        }

        protected override IEnumerable<SyntaxNode> GetClassMembers(TInfo info)
        {
            foreach (SyntaxNode node in GetFields(info))
            {
                yield return node;
            }

            yield return GetConstructor(info, GetConstructorStatements(info));
            yield return GetInitializeMethod(info, GetInitializeMethodStatements(info));
            yield return GetSerializeMethod(info, GetSerializeMethodStatements(info));
            yield return GetDeserializeMethod(info, GetDeserializeMethodStatements(info));
        }

        protected override IEnumerable<SyntaxNode> GetFields(TInfo info)
        {
            foreach (KeyValuePair<string, SyntaxNode> pair in info.InitializeFormatterTypes)
            {
                yield return Generator.FieldDeclaration($"m_formatter{pair.Key}", Generator.GenericName(FormatterInterfaceTypeGenericIdentifier, pair.Value));
            }
        }

        protected override SyntaxNode GetConstructor(TInfo info, IEnumerable<SyntaxNode> statements)
        {
            SyntaxNode providerParameter = Generator.ParameterDeclaration("provider", ProviderType);
            SyntaxNode contextParameter = Generator.ParameterDeclaration("context", ContextType);

            SyntaxNode providerArgument = Generator.Argument(Generator.IdentifierName("provider"));
            SyntaxNode contextArgument = Generator.Argument(Generator.IdentifierName("context"));

            return Generator.ConstructorDeclaration(info.Name, new[] { providerParameter, contextParameter }, Accessibility.Public, DeclarationModifiers.None, new[] { providerArgument, contextArgument }, statements);
        }

        protected override IEnumerable<SyntaxNode> GetConstructorStatements(TInfo info)
        {
            return Enumerable.Empty<SyntaxNode>();
        }

        protected override SyntaxNode GetInitializeMethod(TInfo info, IEnumerable<SyntaxNode> statements)
        {
            return Generator.MethodDeclaration("Initialize", null, null, VoidType, Accessibility.Public, DeclarationModifiers.Override, statements);
        }

        protected override IEnumerable<SyntaxNode> GetInitializeMethodStatements(TInfo info)
        {
            yield return Generator.InvocationExpression(Generator.MemberAccessExpression(Generator.BaseExpression(), "Initialize"));

            foreach (KeyValuePair<string, SyntaxNode> pair in info.InitializeFormatterTypes)
            {
                SyntaxNode providerTryGetAccess = Generator.MemberAccessExpression(Generator.IdentifierName("Provider"), "TryGet");
                SyntaxNode providerTryGetArgument = Generator.Argument(RefKind.Out, Generator.IdentifierName($"m_formatter{pair.Key}"));
                SyntaxNode providerTryGetInvocation = Generator.InvocationExpression(providerTryGetAccess, providerTryGetArgument);

                SyntaxNode stringFormatAccess = Generator.MemberAccessExpression(StringType, "Format");
                SyntaxNode stringFormatArgumentText = Generator.LiteralExpression(ArgumentExceptionText);
                SyntaxNode stringFormatArgumentTypeOfTarget = Generator.TypeOfExpression(pair.Value);
                SyntaxNode stringFormatInvocation = Generator.InvocationExpression(stringFormatAccess, stringFormatArgumentText, stringFormatArgumentTypeOfTarget);

                SyntaxNode exception = Generator.ObjectCreationExpression(ArgumentExceptionType, stringFormatInvocation);

                SyntaxNode condition = Generator.LogicalNotExpression(providerTryGetInvocation);
                SyntaxNode trueStatement = Generator.ThrowStatement(exception);

                yield return Generator.IfStatement(condition, new[] { trueStatement });
            }
        }

        protected override SyntaxNode GetSerializeMethod(TInfo info, IEnumerable<SyntaxNode> statements)
        {
            SyntaxNode writerParameter = Generator.ParameterDeclaration("writer", WriterType, null, RefKind.Ref);
            SyntaxNode valueParameter = Generator.ParameterDeclaration("value", info.TargetType);

            return Generator.MethodDeclaration("Serialize", new[] { writerParameter, valueParameter }, null, VoidType, Accessibility.Public, DeclarationModifiers.Override, statements);
        }

        protected override IEnumerable<SyntaxNode> GetSerializeMethodStatements(TInfo info)
        {
            SyntaxNode writerWriteNilAccess = Generator.MemberAccessExpression(Generator.IdentifierName("writer"), "WriteNil");
            SyntaxNode writerWriteNilInvocation = Generator.InvocationExpression(writerWriteNilAccess);

            SyntaxNode condition = Generator.ValueNotEqualsExpression(Generator.IdentifierName("value"), Generator.DefaultExpression(info.TargetType));

            yield return Generator.IfStatement(condition, GetWriteStatements(info), writerWriteNilInvocation);
        }

        protected override IEnumerable<SyntaxNode> GetWriteStatements(TInfo info)
        {
            return Enumerable.Empty<SyntaxNode>();
        }

        protected override SyntaxNode GetDeserializeMethod(TInfo info, IEnumerable<SyntaxNode> statements)
        {
            SyntaxNode readerParameter = Generator.ParameterDeclaration("reader", ReaderType, null, RefKind.Ref);

            return Generator.MethodDeclaration("Deserialize", new[] { readerParameter }, null, info.TargetType, Accessibility.Public, DeclarationModifiers.Override, statements);
        }

        protected override IEnumerable<SyntaxNode> GetDeserializeMethodStatements(TInfo info)
        {
            SyntaxNode readerTryReadNilAccess = Generator.MemberAccessExpression(Generator.IdentifierName("reader"), "TryReadNil");
            SyntaxNode readerTryReadNilInvocation = Generator.InvocationExpression(readerTryReadNilAccess);

            SyntaxNode condition = Generator.LogicalNotExpression(readerTryReadNilInvocation);

            yield return Generator.IfStatement(condition, GetTrueStatements());
            yield return Generator.ReturnStatement(Generator.DefaultExpression(info.TargetType));

            IEnumerable<SyntaxNode> GetTrueStatements()
            {
                yield return Generator.LocalDeclarationStatement("value", Generator.ObjectCreationExpression(info.TargetType));

                foreach (SyntaxNode node in GetReadStatements(info))
                {
                    yield return node;
                }

                yield return Generator.ReturnStatement(Generator.IdentifierName("value"));
            }
        }

        protected override IEnumerable<SyntaxNode> GetReadStatements(TInfo info)
        {
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}
