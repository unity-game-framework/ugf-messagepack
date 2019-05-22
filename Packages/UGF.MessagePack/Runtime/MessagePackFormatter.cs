using System;

namespace UGF.MessagePack.Runtime
{
    public abstract class MessagePackFormatter<T> : IMessagePackFormatter<T>
    {
        public IMessagePackProvider Provider { get; }
        public IMessagePackContext Context { get; }
        public Type TargetType { get; } = typeof(T);

        protected MessagePackFormatter(IMessagePackProvider provider, IMessagePackContext context)
        {
            Provider = provider;
            Context = context;
        }

        public abstract void Serialize(ref MessagePackWriter writer, T value);
        public abstract T Deserialize(ref MessagePackReader reader);

        void IMessagePackFormatter.Serialize(ref MessagePackWriter writer, object value)
        {
            Serialize(ref writer, (T)value);
        }

        object IMessagePackFormatter.Deserialize(ref MessagePackReader reader)
        {
            return Deserialize(ref reader);
        }
    }
}
