using System;

namespace UGF.MessagePack.Runtime
{
    public interface IMessagePackFormatter
    {
        IMessagePackProvider Provider { get; }
        IMessagePackContext Context { get; }
        Type TargetType { get; }

        void Serialize(ref MessagePackWriter writer, object value);
        object Deserialize(ref MessagePackReader reader);
    }
}
