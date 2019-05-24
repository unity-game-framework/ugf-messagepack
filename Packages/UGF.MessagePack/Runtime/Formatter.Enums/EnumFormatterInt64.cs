using MessagePack;

namespace UGF.MessagePack.Runtime.Formatter.Enums
{
    public class EnumFormatterInt64<T> : MessagePackFormatter<T>
    {
        public EnumFormatterInt64(IMessagePackProvider provider, IMessagePackContext context) : base(provider, context)
        {
        }

        public override void Serialize(ref MessagePackWriter writer, T value)
        {
            writer.WriteInt64(MessagePackUnsafeUtility.As<T, long>(ref value));
        }

        public override T Deserialize(ref MessagePackReader reader)
        {
            long value = reader.ReadInt64();

            return MessagePackUnsafeUtility.As<long, T>(ref value);
        }
    }
}
