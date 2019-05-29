using System;

namespace UGF.MessagePack.Runtime
{
    public interface IMessagePackContext
    {
        T Get<T>();
        object Get(Type type);
        bool TryGet<T>(out T value);
        bool TryGet(Type type, out object value);
    }
}
