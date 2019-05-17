using System;

namespace UGF.MessagePack.Runtime
{
    public interface IMessagePackFormatter
    {
        IMessagePackProvider Provider { get; }
        Type TargetType { get; }

        void Serialize(ref MessagePackWriter writer, object value);
        void Serialize(ref MessagePackWriter writer, object value, IMessagePackContext context);
        object Deserialize(ref MessagePackReader reader);
        object Deserialize(ref MessagePackReader reader, IMessagePackContext context);
    }
}
