using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace UGF.MessagePack.Editor.ExternalType.Analysis
{
    public class MessagePackExternalTypeRewriterAddKeyAttribute : CSharpSyntaxRewriter
    {
        public SyntaxGenerator Generator { get; }
        public SyntaxNode AttributeType { get; }
        public MessagePackExternalTypeInfo Info { get; }

        public MessagePackExternalTypeRewriterAddKeyAttribute(SyntaxGenerator generator, SyntaxNode attributeType, MessagePackExternalTypeInfo info)
        {
            Generator = generator;
            AttributeType = attributeType;
            Info = info;
        }

        public override SyntaxNode VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            string name = node.Declaration.Variables[0].Identifier.Text;

            if (Info.TryGetMember(name, out MessagePackExternalTypeMemberInfo member))
            {
                SyntaxNode attribute = GetKeyAttribute(member);

                return Generator.AddAttributes(node, attribute);
            }

            return base.VisitFieldDeclaration(node);
        }

        public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            string name = node.Identifier.Text;

            if (Info.TryGetMember(name, out MessagePackExternalTypeMemberInfo member))
            {
                SyntaxNode attribute = GetKeyAttribute(member);

                return Generator.AddAttributes(node, attribute);
            }

            return base.VisitPropertyDeclaration(node);
        }

        private SyntaxNode GetKeyAttribute(MessagePackExternalTypeMemberInfo member)
        {
            object value = Info.KeyAsPropertyName ? member.StringKey : (object)member.IntKey;

            return Generator.Attribute(AttributeType, new[]
            {
                Generator.LiteralExpression(value)
            });
        }
    }
}
