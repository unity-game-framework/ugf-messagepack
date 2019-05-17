using System;

namespace UGF.MessagePack.Runtime
{
    public interface IMessagePackProvider
    {
        void Add(IMessagePackFormatter formatter);
        void Remove(IMessagePackFormatter formatter);
        IMessagePackFormatter<T> Get<T>();
        IMessagePackFormatter Get(Type type);
        IMessagePackFormatter<T> GetOrCreate<T>(Func<IMessagePackFormatter> func);
        IMessagePackFormatter GetOrCreate(Type type, Func<IMessagePackFormatter> func);
    }
}
