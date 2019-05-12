using System.Runtime.CompilerServices;
using MessagePack;
using MessagePack.Formatters;

namespace UGF.MessagePack.Runtime.Resolvers.Enums
{
    public class EnumFormatterByte<TValue> : IMessagePackFormatter<TValue>
    {
        public void Serialize(ref MessagePackWriter writer, TValue value, IFormatterResolver resolver)
        {
            writer.Write(Unsafe.As<TValue, byte>(ref value));
        }

        public TValue Deserialize(ref MessagePackReader reader, IFormatterResolver resolver)
        {
            byte value = reader.ReadByte();

            return Unsafe.As<byte, TValue>(ref value);
        }
    }
}
