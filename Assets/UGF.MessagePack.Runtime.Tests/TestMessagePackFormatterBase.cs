using NUnit.Framework;

namespace UGF.MessagePack.Runtime.Tests
{
    public class TestMessagePackFormatterBase
    {
        private class Target
        {
            public bool BoolValue { get; set; } = true;
            public int IntValue { get; set; } = 10;
        }

        private class Formatter : MessagePackFormatterBase<Target>
        {
            public Formatter(IMessagePackProvider provider, IMessagePackContext context) : base(provider, context)
            {
            }

            public override void Serialize(ref MessagePackWriter writer, Target value)
            {
                writer.WriteBoolean(value.BoolValue);
                writer.WriteInt32(value.IntValue);
            }

            public override Target Deserialize(ref MessagePackReader reader)
            {
                var value = new Target();

                value.BoolValue = reader.ReadBoolean();
                value.IntValue = reader.ReadInt32();

                return value;
            }
        }

        [Test]
        public void Serialize()
        {
            var target = new Target();
            var formatter = new Formatter(new MessagePackProvider(), MessagePackContext.Empty);
            var writer = new MessagePackWriter();

            formatter.Serialize(ref writer, target);

            Assert.NotNull(writer.Buffer);
            Assert.AreEqual(2, writer.Position);
            Assert.Pass(writer.Print());
        }

        [Test]
        public void SerializeAsObject()
        {
            var target = new Target();
            IMessagePackFormatter formatter = new Formatter(new MessagePackProvider(), MessagePackContext.Empty);
            var writer = new MessagePackWriter();

            formatter.Serialize(ref writer, target);

            Assert.NotNull(writer.Buffer);
            Assert.AreEqual(2, writer.Position);
            Assert.Pass(writer.Print());
        }

        [Test]
        public void Deserialize()
        {
            var formatter = new Formatter(new MessagePackProvider(), MessagePackContext.Empty);
            var reader = new MessagePackReader(new byte[] { 195, 10 });

            Target target = formatter.Deserialize(ref reader);

            Assert.NotNull(target);
            Assert.AreEqual(true, target.BoolValue);
            Assert.AreEqual(10, target.IntValue);
            Assert.AreEqual(2, reader.Position);
            Assert.Pass(reader.Print());
        }

        [Test]
        public void DeserializeAsObject()
        {
            IMessagePackFormatter formatter = new Formatter(new MessagePackProvider(), MessagePackContext.Empty);
            var reader = new MessagePackReader(new byte[] { 195, 10 });

            var target = formatter.Deserialize(ref reader) as Target;

            Assert.NotNull(target);
            Assert.AreEqual(true, target.BoolValue);
            Assert.AreEqual(10, target.IntValue);
            Assert.AreEqual(2, reader.Position);
            Assert.Pass(reader.Print());
        }
    }
}
