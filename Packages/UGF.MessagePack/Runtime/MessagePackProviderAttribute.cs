using System;
using JetBrains.Annotations;

namespace UGF.MessagePack.Runtime
{
    [BaseTypeRequired(typeof(IMessagePackProvider))]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MessagePackProviderAttribute : Attribute
    {
        public int Order { get; }
        public int Type { get; }

        public MessagePackProviderAttribute(int order, int type)
        {
            Order = order;
            Type = type;
        }

        public MessagePackProviderAttribute(int order, MessagePackFormatterType type)
        {
            Order = order;
            Type = (int)type;
        }
    }
}
