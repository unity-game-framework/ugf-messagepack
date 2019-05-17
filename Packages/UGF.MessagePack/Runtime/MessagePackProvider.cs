using System;
using System.Collections.Generic;

namespace UGF.MessagePack.Runtime
{
    public class MessagePackProvider : IMessagePackProvider
    {
        public Dictionary<Type, IMessagePackFormatter> Formatters { get; } = new Dictionary<Type, IMessagePackFormatter>();

        public void Add(IMessagePackFormatter formatter)
        {
            Formatters.Add(formatter.TargetType, formatter);
        }

        public void Remove(IMessagePackFormatter formatter)
        {
            Formatters.Remove(formatter.TargetType);
        }

        public virtual IMessagePackFormatter<T> Get<T>()
        {
            return (IMessagePackFormatter<T>)Get(typeof(T));
        }

        public virtual IMessagePackFormatter Get(Type type)
        {
            return Formatters[type];
        }

        public virtual IMessagePackFormatter<T> GetOrCreate<T>(Func<IMessagePackFormatter> func)
        {
            return (IMessagePackFormatter<T>)GetOrCreate(typeof(T), func);
        }

        public virtual IMessagePackFormatter GetOrCreate(Type type, Func<IMessagePackFormatter> func)
        {
            if (!Formatters.TryGetValue(type, out IMessagePackFormatter formatter))
            {
                formatter = func();

                Add(formatter);
            }

            return formatter;
        }
    }
}
