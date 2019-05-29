using MessagePack;
using NUnit.Framework;

namespace UGF.MessagePack.Runtime.Tests
{
    public class TestMessagePackReader
    {
        [Test]
        public void TryReadNil()
        {
            var reader = new MessagePackReader(new byte[] { MessagePackCode.Nil, 195 });

            bool result = reader.TryReadNil();

            Assert.True(result);
            Assert.AreEqual(1, reader.Position);
        }

        [Test]
        public void Print()
        {
            var reader = new MessagePackReader(new byte[] { 195, 10 });

            reader.ReadBoolean();
            reader.ReadInt32();

            string actual = reader.Print();
            string expected = "[195, 10]";

            Assert.AreEqual(expected, actual);
        }
    }
}
