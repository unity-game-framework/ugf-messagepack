using System;
using System.Runtime.CompilerServices;
using MessagePack;
using MessagePack.Formatters;

namespace UGF.MessagePack.Runtime.Resolvers.Enums
{
    public class EnumFormatterUInt32<TValue> : IMessagePackFormatter<TValue> where TValue : Enum
    {
        public void Serialize(ref MessagePackWriter writer, TValue value, IFormatterResolver resolver)
        {
            writer.Write(Unsafe.As<TValue, uint>(ref value));
        }

        public TValue Deserialize(ref MessagePackReader reader, IFormatterResolver resolver)
        {
            uint value = reader.ReadUInt32();

            return Unsafe.As<uint, TValue>(ref value);
        }
    }
}
