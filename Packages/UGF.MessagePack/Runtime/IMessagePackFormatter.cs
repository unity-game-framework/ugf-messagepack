using System;

namespace UGF.MessagePack.Runtime
{
    public interface IMessagePackFormatter
    {
        Type TargetType { get; }

        void Serialize(ref MessagePackWriter writer, object value, IMessagePackProvider provider, IMessagePackContext context);
        object Deserialize(ref MessagePackReader reader, IMessagePackProvider provider, IMessagePackContext context);
    }
}
