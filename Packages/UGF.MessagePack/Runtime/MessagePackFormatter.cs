using System;

namespace UGF.MessagePack.Runtime
{
    public abstract class MessagePackFormatter<T> : IMessagePackFormatter<T>, IMessagePackFormatter
    {
        public IMessagePackProvider Provider { get; }
        public Type TargetType { get; } = typeof(T);

        protected MessagePackFormatter(IMessagePackProvider provider)
        {
            Provider = provider;
        }

        public void Serialize(ref MessagePackWriter writer, T value)
        {
            Serialize(ref writer, value, MessagePackContext.Empty);
        }

        public abstract void Serialize(ref MessagePackWriter writer, T value, IMessagePackContext context);

        public T Deserialize(ref MessagePackReader reader)
        {
            return Deserialize(ref reader, MessagePackContext.Empty);
        }

        public abstract T Deserialize(ref MessagePackReader reader, IMessagePackContext context);

        void IMessagePackFormatter.Serialize(ref MessagePackWriter writer, object value)
        {
            Serialize(ref writer, (T)value, MessagePackContext.Empty);
        }

        void IMessagePackFormatter.Serialize(ref MessagePackWriter writer, object value, IMessagePackContext context)
        {
            Serialize(ref writer, (T)value, context);
        }

        object IMessagePackFormatter.Deserialize(ref MessagePackReader reader)
        {
            return Deserialize(ref reader, MessagePackContext.Empty);
        }

        object IMessagePackFormatter.Deserialize(ref MessagePackReader reader, IMessagePackContext context)
        {
            return Deserialize(ref reader, context);
        }
    }
}
