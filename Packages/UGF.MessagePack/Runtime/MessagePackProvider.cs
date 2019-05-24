using System;
using System.Collections.Generic;

namespace UGF.MessagePack.Runtime
{
    public class MessagePackProvider : IMessagePackProvider
    {
        public Dictionary<Type, IMessagePackFormatter> Formatters { get; } = new Dictionary<Type, IMessagePackFormatter>();

        public void Add<T>(IMessagePackFormatter<T> formatter)
        {
            Formatters.Add(formatter.TargetType, formatter);
        }

        public void Remove<T>()
        {
            Formatters.Remove(typeof(T));
        }

        public virtual bool TryGet<T>(out IMessagePackFormatter<T> formatter)
        {
            if (Formatters.TryGetValue(typeof(T), out IMessagePackFormatter formatterBase) && formatterBase is IMessagePackFormatter<T> formatterGeneric)
            {
                formatter = formatterGeneric;
                return true;
            }

            formatter = null;
            return false;
        }

        public virtual bool TryGet(Type type, out IMessagePackFormatter formatter)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return Formatters.TryGetValue(type, out formatter);
        }
    }
}
