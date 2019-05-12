using System.Runtime.CompilerServices;
using MessagePack;
using MessagePack.Formatters;

namespace UGF.MessagePack.Runtime.Resolvers.Enums
{
    public class EnumFormatterUInt16<TValue> : IMessagePackFormatter<TValue>
    {
        public void Serialize(ref MessagePackWriter writer, TValue value, IFormatterResolver resolver)
        {
            writer.Write(Unsafe.As<TValue, ushort>(ref value));
        }

        public TValue Deserialize(ref MessagePackReader reader, IFormatterResolver resolver)
        {
            ushort value = reader.ReadUInt16();

            return Unsafe.As<ushort, TValue>(ref value);
        }
    }
}
