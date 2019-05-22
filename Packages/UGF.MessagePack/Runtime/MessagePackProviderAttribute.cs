using System;
using JetBrains.Annotations;

namespace UGF.MessagePack.Runtime
{
    [BaseTypeRequired(typeof(IMessagePackProvider))]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MessagePackProviderAttribute : Attribute
    {
        public int Priority { get; }
        public int Type { get; }

        public MessagePackProviderAttribute(int priority, int type)
        {
            Priority = priority;
            Type = type;
        }

        public MessagePackProviderAttribute(int priority, MessagePackFormatterType type)
        {
            Priority = priority;
            Type = (int)type;
        }
    }
}
