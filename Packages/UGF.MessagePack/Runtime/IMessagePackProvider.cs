using System;

namespace UGF.MessagePack.Runtime
{
    public interface IMessagePackProvider
    {
        bool TryGet<T>(out IMessagePackFormatter<T> formatter);
        bool TryGet(Type type, out IMessagePackFormatter formatter);
    }
}
