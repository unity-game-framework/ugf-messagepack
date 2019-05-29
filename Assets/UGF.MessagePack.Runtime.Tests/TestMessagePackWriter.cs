using NUnit.Framework;

namespace UGF.MessagePack.Runtime.Tests
{
    public class TestMessagePackWriter
    {
        [Test]
        public void Print()
        {
            var writer = new MessagePackWriter();

            writer.WriteBoolean(true);
            writer.WriteInt32(10);

            string actual = writer.Print();
            string expected = "[195, 10]";

            Assert.AreEqual(expected, actual);
        }
    }
}
