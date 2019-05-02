using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace UGF.MessagePack.Editor.Analysis
{
    internal class MessagePackRewriterAddFormatterAttribute : CSharpSyntaxRewriter
    {
        public SyntaxGenerator Generator { get; }
        public TypeSyntax AttributeType { get; }

        public MessagePackRewriterAddFormatterAttribute(SyntaxGenerator generator, TypeSyntax attributeType)
        {
            Generator = generator ?? throw new ArgumentNullException(nameof(generator));
            AttributeType = attributeType ?? throw new ArgumentNullException(nameof(attributeType));
        }

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (node.BaseList != null)
            {
                for (int i = 0; i < node.BaseList.Types.Count; i++)
                {
                    TypeSyntax type = node.BaseList.Types[i].Type;

                    if (type is NameSyntax nameSyntax)
                    {
                        if (type is QualifiedNameSyntax qualifiedNameSyntax)
                        {
                            nameSyntax = qualifiedNameSyntax.Right;
                        }

                        if (nameSyntax is GenericNameSyntax genericNameSyntax && genericNameSyntax.Identifier.Text == "IMessagePackFormatter")
                        {
                            if (genericNameSyntax.TypeArgumentList.Arguments.Count == 1)
                            {
                                TypeSyntax targetType = genericNameSyntax.TypeArgumentList.Arguments[0];
                                SyntaxNode attribute = Generator.Attribute(AttributeType, new[]
                                {
                                    Generator.TypeOfExpression(targetType)
                                });

                                return Generator.AddAttributes(node, attribute);
                            }
                        }
                    }
                }
            }

            return base.VisitClassDeclaration(node);
        }
    }
}
