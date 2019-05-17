using System;

namespace UGF.MessagePack.Runtime
{
    public abstract class MessagePackFormatter<T> : IMessagePackFormatter<T>
    {
        public Type TargetType { get; } = typeof(T);

        public abstract void Serialize(ref MessagePackWriter writer, T value, IMessagePackProvider provider, IMessagePackContext context);
        public abstract T Deserialize(ref MessagePackReader reader, IMessagePackProvider provider, IMessagePackContext context);

        void IMessagePackFormatter.Serialize(ref MessagePackWriter writer, object value, IMessagePackProvider provider, IMessagePackContext context)
        {
            Serialize(ref writer, (T)value, provider, context);
        }

        object IMessagePackFormatter.Deserialize(ref MessagePackReader reader, IMessagePackProvider provider, IMessagePackContext context)
        {
            return Deserialize(ref reader, provider, context);
        }
    }
}
