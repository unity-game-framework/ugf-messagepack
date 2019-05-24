using MessagePack;

namespace UGF.MessagePack.Runtime.Formatter.Enums
{
    public class EnumFormatterUInt16<T> : MessagePackFormatterBase<T>
    {
        public EnumFormatterUInt16(IMessagePackProvider provider, IMessagePackContext context) : base(provider, context)
        {
        }

        public override void Serialize(ref MessagePackWriter writer, T value)
        {
            writer.WriteUInt16(MessagePackUnsafeUtility.As<T, ushort>(ref value));
        }

        public override T Deserialize(ref MessagePackReader reader)
        {
            ushort value = reader.ReadUInt16();

            return MessagePackUnsafeUtility.As<ushort, T>(ref value);
        }
    }
}
