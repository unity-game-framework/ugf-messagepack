using NUnit.Framework;
using UGF.MessagePack.Runtime.Formatter.Enums;

namespace UGF.MessagePack.Runtime.Tests.Formatter.Enums
{
    public class TestEnumFormatterInt64
    {
        private enum Target : long
        {
            Value
        }

        [Test]
        public void Serialize()
        {
            var target = Target.Value;
            var formatter = new EnumFormatterInt64<Target>(new MessagePackProvider(), MessagePackContext.Empty);
            var writer = new MessagePackWriter();

            formatter.Serialize(ref writer, target);

            Assert.NotNull(writer.Buffer);
            Assert.AreEqual(1, writer.Position);
            Assert.Pass(writer.Print());
        }

        [Test]
        public void Deserialize()
        {
            var formatter = new EnumFormatterInt64<Target>(new MessagePackProvider(), MessagePackContext.Empty);
            var reader = new MessagePackReader(new byte[] { 0 });

            Target target = formatter.Deserialize(ref reader);

            Assert.AreEqual(Target.Value, target);
            Assert.Pass(reader.Print());
        }
    }
}
