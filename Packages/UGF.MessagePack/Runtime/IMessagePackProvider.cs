using System;

namespace UGF.MessagePack.Runtime
{
    public interface IMessagePackProvider
    {
        IMessagePackFormatter<T> Get<T>();
        IMessagePackFormatter Get(Type type);
    }
}
