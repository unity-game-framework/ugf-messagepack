using System;
using MessagePack;
using MessagePack.Internal;

namespace UGF.MessagePack.Runtime.Tests.TestAssembly
{
    [MessagePackFormatter(MessagePackFormatterType.Named)]
    public class TestTargetFormatterNamed : MessagePackFormatterBase<TestTarget>
    {
        private readonly AutomataDictionary m_nameToKey = new AutomataDictionary
        {
            { "BoolValue", 0 },
            { "IntValue", 1 },
            { "FloatValue", 2 },
            { "StringValue", 3 },
            { "EnumValue", 4 }
        };

        private readonly byte[][] m_keyToBytes =
        {
            MessagePackBinary.GetEncodedStringBytes("BoolValue"),
            MessagePackBinary.GetEncodedStringBytes("IntValue"),
            MessagePackBinary.GetEncodedStringBytes("FloatValue"),
            MessagePackBinary.GetEncodedStringBytes("StringValue"),
            MessagePackBinary.GetEncodedStringBytes("EnumValue"),
        };

        private IMessagePackFormatter<TypeCode> m_formatterTypeCode;

        public TestTargetFormatterNamed(IMessagePackProvider provider, IMessagePackContext context) : base(provider, context)
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
                writer.WriteMapHeader(5);
                writer.WriteBytes(m_keyToBytes[0]);
                writer.WriteBoolean(value.BoolValue);
                writer.WriteBytes(m_keyToBytes[1]);
                writer.WriteInt32(value.IntValue);
                writer.WriteBytes(m_keyToBytes[2]);
                writer.WriteSingle(value.FloatValue);
                writer.WriteBytes(m_keyToBytes[3]);
                writer.WriteStringUnsafe(value.StringValue);
                writer.WriteBytes(m_keyToBytes[4]);
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
                int count = reader.ReadMapHeader();

                for (int i = 0; i < count; i++)
                {
                    ArraySegment<byte> segment = reader.ReadStringSegment();

                    if (m_nameToKey.TryGetValue(segment.Array, segment.Offset, segment.Count, out int key))
                    {
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
                    else
                    {
                        reader.ReadNextBlock();
                    }
                }
            }

            return default;
        }
    }
}
