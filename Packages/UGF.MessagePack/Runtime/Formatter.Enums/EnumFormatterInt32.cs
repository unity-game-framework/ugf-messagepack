using MessagePack;

namespace UGF.MessagePack.Runtime.Formatter.Enums
{
    public class EnumFormatterInt32<T> : MessagePackFormatter<T>
    {
        public EnumFormatterInt32(IMessagePackProvider provider, IMessagePackContext context) : base(provider, context)
        {
        }

        public override void Serialize(ref MessagePackWriter writer, T value)
        {
            writer.WriteInt32(MessagePackUnsafeUtility.As<T, int>(ref value));
        }

        public override T Deserialize(ref MessagePackReader reader)
        {
            int value = reader.ReadInt32();

            return MessagePackUnsafeUtility.As<int, T>(ref value);
        }
    }
}
