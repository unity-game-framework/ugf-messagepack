using System;
using JetBrains.Annotations;

namespace UGF.MessagePack.Runtime
{
    [BaseTypeRequired(typeof(IMessagePackProvider))]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MessagePackProviderAttribute : Attribute
    {
        public int Priority { get; }
        public MessagePackFormatterType Type { get; }

        public MessagePackProviderAttribute(int priority, MessagePackFormatterType type)
        {
            Priority = priority;
            Type = type;
        }
    }
}
