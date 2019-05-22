using System;
using JetBrains.Annotations;

namespace UGF.MessagePack.Runtime
{
    [BaseTypeRequired(typeof(IMessagePackFormatter))]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MessagePackFormatterAttribute : Attribute
    {
        public MessagePackFormatterType Type { get; }

        public MessagePackFormatterAttribute(MessagePackFormatterType type)
        {
            Type = type;
        }
    }
}
