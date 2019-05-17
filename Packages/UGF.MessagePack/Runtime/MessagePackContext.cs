using System;
using System.Collections.Generic;

namespace UGF.MessagePack.Runtime
{
    public class MessagePackContext : IMessagePackContext
    {
        public Dictionary<Type, object> Values { get; } = new Dictionary<Type, object>();

        public T Get<T>()
        {
            return (T)Values[typeof(T)];
        }

        public object Get(Type type)
        {
            return Values[type];
        }

        public bool TryGet<T>(out T value)
        {
            if (Values.TryGetValue(typeof(T), out object result))
            {
                value = (T)result;
                return true;
            }

            value = default;
            return false;
        }

        public bool TryGet(Type type, out object value)
        {
            return Values.TryGetValue(type, out value);
        }
    }
}
