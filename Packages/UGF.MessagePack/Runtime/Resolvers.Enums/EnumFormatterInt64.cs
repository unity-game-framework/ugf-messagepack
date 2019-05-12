using System.Runtime.CompilerServices;
using MessagePack;
using MessagePack.Formatters;

namespace UGF.MessagePack.Runtime.Resolvers.Enums
{
    public class EnumFormatterInt64<TValue> : IMessagePackFormatter<TValue>
    {
        public void Serialize(ref MessagePackWriter writer, TValue value, IFormatterResolver resolver)
        {
            writer.Write(Unsafe.As<TValue, long>(ref value));
        }

        public TValue Deserialize(ref MessagePackReader reader, IFormatterResolver resolver)
        {
            long value = reader.ReadInt64();

            return Unsafe.As<long, TValue>(ref value);
        }
    }
}
