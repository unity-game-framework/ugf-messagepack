using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace UGF.MessagePack.Runtime.Tests
{
    public class TestSerialization
    {
        public class Target
        {
            public bool BoolValue { get; set; } = true;
            public int IntValue { get; set; } = 10;
            public float FloatValue { get; set; } = 10.5F;
            public string StringValue { get; set; } = "Text";
            public TypeCode EnumValue { get; set; } = TypeCode.Boolean;
            public List<int> IntList { get; set; } = new List<int>();
            public List<TypeCode> EnumList { get; set; } = new List<TypeCode>();
            public Target TargetValue { get; set; }
            public List<Target> TargetList { get; set; } = new List<Target>();
        }

        [MessagePackFormatter(-2)]
        public class TargetFormatter : MessagePackFormatterBase<Target>
        {
            private IMessagePackFormatter<TypeCode> m_formatterTypeCode;
            private IMessagePackFormatter<List<int>> m_formatterListInt;
            private IMessagePackFormatter<List<TypeCode>> m_formatterListTypeCode;
            private IMessagePackFormatter<Target> m_formatterTarget;
            private IMessagePackFormatter<List<Target>> m_formatterListTarget;

            public TargetFormatter(IMessagePackProvider provider, IMessagePackContext context) : base(provider, context)
            {
            }

            public override void Initialize()
            {
                base.Initialize();

                m_formatterTypeCode = Provider.Get<TypeCode>();
                m_formatterListInt = Provider.Get<List<int>>();
                m_formatterListTypeCode = Provider.Get<List<TypeCode>>();
                m_formatterTarget = Provider.Get<Target>();
                m_formatterListTarget = Provider.Get<List<Target>>();
            }

            public override void Serialize(ref MessagePackWriter writer, Target value)
            {
                if (value != null)
                {
                    writer.WriteBoolean(value.BoolValue);
                    writer.WriteInt32(value.IntValue);
                    writer.WriteSingle(value.FloatValue);
                    writer.WriteStringUnsafe(value.StringValue);
                    m_formatterTypeCode.Serialize(ref writer, value.EnumValue);
                    m_formatterListInt.Serialize(ref writer, value.IntList);
                    m_formatterListTypeCode.Serialize(ref writer, value.EnumList);
                    m_formatterTarget.Serialize(ref writer, value.TargetValue);
                    m_formatterListTarget.Serialize(ref writer, value.TargetList);
                }
                else
                {
                    writer.WriteNil();
                }
            }

            public override Target Deserialize(ref MessagePackReader reader)
            {
                if (!reader.TryReadNil())
                {
                    var value = new Target();

                    value.BoolValue = reader.ReadBoolean();
                    value.IntValue = reader.ReadInt32();
                    value.FloatValue = reader.ReadSingle();
                    value.StringValue = reader.ReadString();
                    value.EnumValue = m_formatterTypeCode.Deserialize(ref reader);
                    value.IntList = m_formatterListInt.Deserialize(ref reader);
                    value.EnumList = m_formatterListTypeCode.Deserialize(ref reader);
                    value.TargetValue = m_formatterTarget.Deserialize(ref reader);
                    value.TargetList = m_formatterListTarget.Deserialize(ref reader);

                    return value;
                }

                return null;
            }
        }

        [Test]
        public void Serialize()
        {
            IMessagePackProvider provider = MessagePackUtility.CreateProvider(MessagePackContext.Empty, -2);
            IMessagePackFormatter<Target> formatter = provider.Get<Target>();

            var target = new Target { TargetValue = new Target() };
            var writer = new MessagePackWriter();

            formatter.Serialize(ref writer, target);

            Assert.NotNull(writer.Buffer);
            Assert.Pass(writer.Print());
        }
    }
}
