using MessagePack;

namespace UGF.MessagePack.Runtime.Formatter.Enums
{
    public class EnumFormatterUInt32<T> : MessagePackFormatter<T>
    {
        public EnumFormatterUInt32(IMessagePackProvider provider, IMessagePackContext context) : base(provider, context)
        {
        }

        public override void Serialize(ref MessagePackWriter writer, T value)
        {
            writer.WriteUInt32(MessagePackUnsafeUtility.As<T, uint>(ref value));
        }

        public override T Deserialize(ref MessagePackReader reader)
        {
            uint value = reader.ReadUInt32();

            return MessagePackUnsafeUtility.As<uint, T>(ref value);
        }
    }
}
