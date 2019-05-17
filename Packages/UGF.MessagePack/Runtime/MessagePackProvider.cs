using System;
using System.Collections.Generic;

namespace UGF.MessagePack.Runtime
{
    public class MessagePackProvider : IMessagePackProvider
    {
        public Dictionary<Type, IMessagePackFormatter> Formatters { get; } = new Dictionary<Type, IMessagePackFormatter>();

        public void Add(IMessagePackFormatter formatter)
        {
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));

            Formatters.Add(formatter.TargetType, formatter);
        }

        public void Remove(IMessagePackFormatter formatter)
        {
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));

            Formatters.Remove(formatter.TargetType);
        }

        public virtual IMessagePackFormatter<T> Get<T>()
        {
            return (IMessagePackFormatter<T>)Get(typeof(T));
        }

        public virtual IMessagePackFormatter Get(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return Formatters[type];
        }

        public virtual IMessagePackFormatter<T> GetOrCreate<T>(MessagePackFormatterCreateHandler handler)
        {
            return (IMessagePackFormatter<T>)GetOrCreate(typeof(T), handler);
        }

        public virtual IMessagePackFormatter GetOrCreate(Type type, MessagePackFormatterCreateHandler handler)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            if (!Formatters.TryGetValue(type, out IMessagePackFormatter formatter))
            {
                formatter = handler();

                Add(formatter);
            }

            return formatter;
        }
    }
}
