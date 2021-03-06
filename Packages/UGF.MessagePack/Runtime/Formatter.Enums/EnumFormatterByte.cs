using MessagePack;

namespace UGF.MessagePack.Runtime.Formatter.Enums
{
    public class EnumFormatterByte<T> : MessagePackFormatterBase<T>
    {
        public EnumFormatterByte(IMessagePackProvider provider, IMessagePackContext context) : base(provider, context)
        {
        }

        public override void Serialize(ref MessagePackWriter writer, T value)
        {
            writer.WriteByte(MessagePackUnsafeUtility.As<T, byte>(ref value));
        }

        public override T Deserialize(ref MessagePackReader reader)
        {
            byte value = reader.ReadByte();

            return MessagePackUnsafeUtility.As<byte, T>(ref value);
        }
    }
}
