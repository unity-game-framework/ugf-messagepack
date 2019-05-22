using System;
using System.Collections.Generic;

namespace UGF.MessagePack.Runtime
{
    public class MessagePackProvider : IMessagePackProvider
    {
        public Dictionary<Type, IMessagePackFormatter> Formatters { get; } = new Dictionary<Type, IMessagePackFormatter>();

        public virtual IMessagePackFormatter<T> Get<T>()
        {
            return (IMessagePackFormatter<T>)Get(typeof(T));
        }

        public virtual IMessagePackFormatter Get(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return Formatters[type];
        }
    }
}
