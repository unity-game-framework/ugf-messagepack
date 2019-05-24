using MessagePack;
using MessagePack.Resolvers;
using NUnit.Framework;

namespace UGF.MessagePack.Runtime.Tests
{
    public class TestMessagePackFormatterWrapper
    {
        private class Target
        {
            public bool BoolValue { get; set; } = true;
            public int IntValue { get; set; } = 10;
        }

        private class Formatter : global::MessagePack.Formatters.IMessagePackFormatter<Target>
        {
            public int Serialize(ref byte[] bytes, int offset, Target value, IFormatterResolver formatterResolver)
            {
                int start = offset;

                offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.BoolValue);
                offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.IntValue);

                return offset - start;
            }

            public Target Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
            {
                int start = offset;
                var value = new Target();

                value.BoolValue = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                offset += readSize;
                value.IntValue = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                offset += readSize;

                readSize = offset - start;
                return value;
            }
        }

        [Test]
        public void Serialize()
        {
            var target = new Target();
            var formatter = new MessagePackFormatterWrapper<Target>(new MessagePackProvider(), MessagePackContext.Empty, new Formatter(), BuiltinResolver.Instance);
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
            IMessagePackFormatter formatter = new MessagePackFormatterWrapper<Target>(new MessagePackProvider(), MessagePackContext.Empty, new Formatter(), BuiltinResolver.Instance);
            var writer = new MessagePackWriter();

            formatter.Serialize(ref writer, target);

            Assert.NotNull(writer.Buffer);
            Assert.AreEqual(2, writer.Position);
            Assert.Pass(writer.Print());
        }

        [Test]
        public void Deserialize()
        {
            var formatter = new MessagePackFormatterWrapper<Target>(new MessagePackProvider(), MessagePackContext.Empty, new Formatter(), BuiltinResolver.Instance);
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
            IMessagePackFormatter formatter = new MessagePackFormatterWrapper<Target>(new MessagePackProvider(), MessagePackContext.Empty, new Formatter(), BuiltinResolver.Instance);
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
