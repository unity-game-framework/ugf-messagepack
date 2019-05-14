using System.Collections.Generic;
using MessagePack;
using NUnit.Framework;
using UnityEngine;

namespace UGF.MessagePack.Runtime.Tests
{
    public class TestSerialization
    {
        private MessagePackSerializer m_serializer;

        [MessagePackObject]
        public class Target : ITargetUnion
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

        [MessagePackObject]
        public class Target2 : ITargetUnion
        {
            [Key(0)]
            public string Name { get; set; } = "Target";

            [Key(1)]
            public Vector2 Vector2 { get; set; } = Vector2.one;

            [Key(2)]
            public Bounds Bounds { get; set; } = new Bounds(Vector3.one, Vector3.one);

            [Key(3)]
            public List<Target> Targets { get; set; }

            [Key(4)]
            public ITargetUnion Union { get; set; }
        }

        [Union(0, typeof(Target))]
        [Union(1, typeof(Target2))]
        public interface ITargetUnion
        {
            [Key(0)]
            string Name { get; set; }
        }

        [MessagePackObject(true)]
        public class Target3
        {
            public string Name { get; set; } = "Target";
            public bool BoolValue { get; set; } = true;
            public float FloatValue { get; set; } = 50.5F;
            public int IntValue { get; set; } = 50;
            public HideFlags Flags { get; set; } = HideFlags.DontSave;
            public ITargetUnion Union { get; set; } = new Target();
        }

        [SetUp]
        public void Setup()
        {
            m_serializer = new MessagePackSerializer(MessagePackUtility.CreateDefaultResolver());
        }

        [Test]
        public void Serialize()
        {
            var target = new Target();

            byte[] bytes = m_serializer.Serialize(target);

            Assert.NotNull(bytes);
        }

        [Test]
        public void Serialize2()
        {
            var target = new Target2();

            byte[] bytes = m_serializer.Serialize(target);

            Assert.NotNull(bytes);
        }

        [Test]
        public void Serialize3()
        {
            var target = new Target3();

            string text = m_serializer.SerializeToJson(target);

            Assert.NotNull(text);
            Debug.Log(text);
        }
    }
}
