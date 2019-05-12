using System.Runtime.CompilerServices;
using MessagePack;
using MessagePack.Formatters;

namespace UGF.MessagePack.Runtime.Resolvers.Enums
{
    public class EnumFormatterSByte<TValue> : IMessagePackFormatter<TValue>
    {
        public void Serialize(ref MessagePackWriter writer, TValue value, IFormatterResolver resolver)
        {
            writer.Write(Unsafe.As<TValue, sbyte>(ref value));
        }

        public TValue Deserialize(ref MessagePackReader reader, IFormatterResolver resolver)
        {
            sbyte value = reader.ReadSByte();

            return Unsafe.As<sbyte, TValue>(ref value);
        }
    }
}
