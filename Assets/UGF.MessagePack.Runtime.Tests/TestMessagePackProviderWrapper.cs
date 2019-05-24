using MessagePack.Resolvers;
using NUnit.Framework;

namespace UGF.MessagePack.Runtime.Tests
{
    public class TestMessagePackProviderWrapper
    {
        [Test]
        public void TryGetT()
        {
            var provider = new MessagePackProviderWrapper(MessagePackContext.Empty, BuiltinResolver.Instance);

            bool result0 = provider.TryGet(out IMessagePackFormatter<bool> formatter0);
            bool result1 = provider.TryGet(out IMessagePackFormatter<int> formatter1);

            Assert.True(result0);
            Assert.True(result1);
            Assert.NotNull(formatter0);
            Assert.NotNull(formatter1);
            Assert.IsAssignableFrom<MessagePackFormatterWrapper<bool>>(formatter0);
            Assert.IsAssignableFrom<MessagePackFormatterWrapper<int>>(formatter1);
        }

        [Test]
        public void TryGet()
        {
            var provider = new MessagePackProviderWrapper(MessagePackContext.Empty, BuiltinResolver.Instance);

            bool result0 = provider.TryGet(typeof(bool), out IMessagePackFormatter formatter0);
            bool result1 = provider.TryGet(typeof(int), out IMessagePackFormatter formatter1);

            Assert.True(result0);
            Assert.True(result1);
            Assert.NotNull(formatter0);
            Assert.NotNull(formatter1);
            Assert.IsAssignableFrom<MessagePackFormatterWrapper<bool>>(formatter0);
            Assert.IsAssignableFrom<MessagePackFormatterWrapper<int>>(formatter1);
        }
    }
}
