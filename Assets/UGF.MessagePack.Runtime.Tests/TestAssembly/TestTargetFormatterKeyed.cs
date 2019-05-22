using System;

namespace UGF.MessagePack.Runtime.Tests.TestAssembly
{
    [MessagePackFormatter(MessagePackFormatterType.Keyed)]
    public sealed class TestTargetFormatterKeyed : MessagePackFormatter<TestTarget>
    {
        private readonly IMessagePackFormatter<TypeCode> m_formatterTypeCode;

        public TestTargetFormatterKeyed(IMessagePackProvider provider, IMessagePackContext context) : base(provider, context)
        {
            m_formatterTypeCode = provider.Get<TypeCode>() ?? throw new ArgumentException($"The formatter for the specified type not found: '{typeof(TypeCode)}'.");
        }

        public override void Serialize(ref MessagePackWriter writer, TestTarget value)
        {
            if (value != default)
            {
                writer.WriteUInt32(5U);
                writer.WriteUInt32(0U);
                writer.WriteBoolean(value.BoolValue);
                writer.WriteUInt32(1U);
                writer.WriteInt32(value.IntValue);
                writer.WriteUInt32(2U);
                writer.WriteSingle(value.FloatValue);
                writer.WriteUInt32(3U);
                writer.WriteStringUnsafe(value.StringValue);
                writer.WriteUInt32(4U);
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
                uint count = reader.ReadUInt32();

                for (uint i = 0; i < count; i++)
                {
                    uint key = reader.ReadUInt32();

                    switch (key)
                    {
                        case 0U:
                        {
                            value.BoolValue = reader.ReadBoolean();
                            break;
                        }
                        case 1U:
                        {
                            value.IntValue = reader.ReadInt32();
                            break;
                        }
                        case 2U:
                        {
                            value.FloatValue = reader.ReadSingle();
                            break;
                        }
                        case 3U:
                        {
                            value.StringValue = reader.ReadString();
                            break;
                        }
                        case 4U:
                        {
                            value.EnumValue = m_formatterTypeCode.Deserialize(ref reader);
                            break;
                        }
                        default:
                        {
                            reader.ReadNextBlock();
                            break;
                        }
                    }
                }

                return value;
            }

            return default;
        }
    }
}
