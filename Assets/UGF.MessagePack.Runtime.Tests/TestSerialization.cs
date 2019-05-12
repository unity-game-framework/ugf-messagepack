using MessagePack;
using NUnit.Framework;
using UnityEngine;

namespace UGF.MessagePack.Runtime.Tests
{
    public class TestSerialization
    {
        private MessagePackSerializer m_serializer;

        [MessagePackObject]
        public class Target
        {
            [Key(0)]
            public string Name { get; set; } = "Target";

            [Key(1)]
            public bool BoolValue { get; set; } = true;

            [Key(2)]
            public float FloatValue { get; set; } = 50.5F;

            [Key(3)]
            public int IntValue { get; set; } = 50;

            [Key(4)]
            public HideFlags Flags { get; set; } = HideFlags.DontSave;
        }

        [SetUp]
        public void Setup()
        {
            MessagePackFormatterResolver resolver = MessagePackUtility.CreateDefaultResolver();

            resolver.CacheFormatters();

            m_serializer = new MessagePackSerializer(resolver);
        }

        [Test]
        public void Serialize()
        {
            var target = new Target();

            byte[] bytes = m_serializer.Serialize(target);

            Assert.NotNull(bytes);
        }
    }
}
