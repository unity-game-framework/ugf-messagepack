using MessagePack;

namespace UGF.MessagePack.Runtime.Formatter.Enums
{
    public class EnumFormatterUInt64<T> : MessagePackFormatterBase<T>
    {
        public EnumFormatterUInt64(IMessagePackProvider provider, IMessagePackContext context) : base(provider, context)
        {
        }

        public override void Serialize(ref MessagePackWriter writer, T value)
        {
            writer.WriteUInt64(MessagePackUnsafeUtility.As<T, ulong>(ref value));
        }

        public override T Deserialize(ref MessagePackReader reader)
        {
            ulong value = reader.ReadUInt64();

            return MessagePackUnsafeUtility.As<ulong, T>(ref value);
        }
    }
}
