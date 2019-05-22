using System;

namespace UGF.MessagePack.Runtime.Tests.TestAssembly
{
    [MessagePackFormatter(MessagePackFormatterType.Forward)]
    public sealed class TestTargetFormatterForward : MessagePackFormatter<TestTarget>
    {
        private readonly IMessagePackFormatter<TypeCode> m_formatterTypeCode;

        public TestTargetFormatterForward(IMessagePackProvider provider, IMessagePackContext context) : base(provider, context)
        {
            m_formatterTypeCode = provider.Get<TypeCode>() ?? throw new ArgumentException($"The formatter for the specified type not found: '{typeof(TypeCode)}'.");
        }

        public override void Serialize(ref MessagePackWriter writer, TestTarget value)
        {
            if (value != default)
            {
                writer.WriteBoolean(value.BoolValue);
                writer.WriteInt32(value.IntValue);
                writer.WriteSingle(value.FloatValue);
                writer.WriteStringUnsafe(value.StringValue);
                m_formatterTypeCode.Serialize(ref writer, value.EnumValue);
            }
            else
            {
                writer.WriteNil();
            }
        }

        public override TestTarget Deserialize(ref MessagePackReader reader)
        {
            if (!reader.TryReadNil())
            {
                var value = new TestTarget();

                value.BoolValue = reader.ReadBoolean();
                value.IntValue = reader.ReadInt32();
                value.FloatValue = reader.ReadSingle();
                value.StringValue = reader.ReadString();
                value.EnumValue = m_formatterTypeCode.Deserialize(ref reader);

                return value;
            }

            return default;
        }
    }
}
