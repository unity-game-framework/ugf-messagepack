using System.Runtime.CompilerServices;
using MessagePack;
using MessagePack.Formatters;

namespace UGF.MessagePack.Runtime.Resolvers.Enums
{
    public class EnumFormatterInt32<TValue> : IMessagePackFormatter<TValue>
    {
        public void Serialize(ref MessagePackWriter writer, TValue value, IFormatterResolver resolver)
        {
            writer.Write(Unsafe.As<TValue, int>(ref value));
        }

        public TValue Deserialize(ref MessagePackReader reader, IFormatterResolver resolver)
        {
            int value = reader.ReadInt32();

            return Unsafe.As<int, TValue>(ref value);
        }
    }
}
