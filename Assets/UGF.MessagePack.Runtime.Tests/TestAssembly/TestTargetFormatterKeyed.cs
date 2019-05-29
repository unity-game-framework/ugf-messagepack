using System;

namespace UGF.MessagePack.Runtime.Tests.TestAssembly
{
    [MessagePackFormatter(MessagePackFormatterType.Keyed)]
    public sealed class TestTargetFormatterKeyed : MessagePackFormatterBase<TestTarget>
    {
        private IMessagePackFormatter<TypeCode> m_formatterTypeCode;

        public TestTargetFormatterKeyed(IMessagePackProvider provider, IMessagePackContext context) : base(provider, context)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            m_formatterTypeCode = Provider.Get<TypeCode>();
        }

        public override void Serialize(ref MessagePackWriter writer, TestTarget value)
        {
            if (value != default)
            {
                writer.WriteArrayHeader(5);
                writer.WriteInt32(0);
                writer.WriteBoolean(value.BoolValue);
                writer.WriteInt32(1);
                writer.WriteInt32(value.IntValue);
                writer.WriteInt32(2);
                writer.WriteSingle(value.FloatValue);
                writer.WriteInt32(3);
                writer.WriteStringUnsafe(value.StringValue);
                writer.WriteInt32(4);
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
                int count = reader.ReadArrayHeader();

                for (int i = 0; i < count; i++)
                {
                    int key = reader.ReadInt32();

                    switch (key)
                    {
                        case 0:
                        {
                            value.BoolValue = reader.ReadBoolean();
                            break;
                        }
                        case 1:
                        {
                            value.IntValue = reader.ReadInt32();
                            break;
                        }
                        case 2:
                        {
                            value.FloatValue = reader.ReadSingle();
                            break;
                        }
                        case 3:
                        {
                            value.StringValue = reader.ReadString();
                            break;
                        }
                        case 4:
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
