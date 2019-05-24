using MessagePack;

namespace UGF.MessagePack.Runtime.Formatter.Enums
{
    public class EnumFormatterSByte<T> : MessagePackFormatter<T>
    {
        public EnumFormatterSByte(IMessagePackProvider provider, IMessagePackContext context) : base(provider, context)
        {
        }

        public override void Serialize(ref MessagePackWriter writer, T value)
        {
            writer.WriteSByte(MessagePackUnsafeUtility.As<T, sbyte>(ref value));
        }

        public override T Deserialize(ref MessagePackReader reader)
        {
            sbyte value = reader.ReadSByte();

            return MessagePackUnsafeUtility.As<sbyte, T>(ref value);
        }
    }
}
