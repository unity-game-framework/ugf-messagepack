using System;
using JetBrains.Annotations;

namespace UGF.MessagePack.Runtime
{
    [BaseTypeRequired(typeof(IMessagePackFormatter))]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MessagePackFormatterAttribute : Attribute
    {
        public int Type { get; }

        public MessagePackFormatterAttribute(int type)
        {
            Type = type;
        }

        public MessagePackFormatterAttribute(MessagePackFormatterType type)
        {
            Type = (int)type;
        }
    }
}
