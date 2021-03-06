using MessagePack;

namespace UGF.MessagePack.Runtime.Formatter.Enums
{
    public class EnumFormatterInt16<T> : MessagePackFormatterBase<T>
    {
        public EnumFormatterInt16(IMessagePackProvider provider, IMessagePackContext context) : base(provider, context)
        {
        }

        public override void Serialize(ref MessagePackWriter writer, T value)
        {
            writer.WriteInt16(MessagePackUnsafeUtility.As<T, short>(ref value));
        }

        public override T Deserialize(ref MessagePackReader reader)
        {
            short value = reader.ReadInt16();

            return MessagePackUnsafeUtility.As<short, T>(ref value);
        }
    }
}
