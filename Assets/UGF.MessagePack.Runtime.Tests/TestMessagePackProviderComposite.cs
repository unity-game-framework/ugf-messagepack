using NUnit.Framework;

namespace UGF.MessagePack.Runtime.Tests
{
    public class TestMessagePackProviderComposite
    {
        private class Target
        {
        }

        private class Formatter : MessagePackFormatter<Target>
        {
            public Formatter(IMessagePackProvider provider, IMessagePackContext context) : base(provider, context)
            {
            }

            public override void Serialize(ref MessagePackWriter writer, Target value)
            {
            }

            public override Target Deserialize(ref MessagePackReader reader)
            {
                return new Target();
            }
        }

        [Test]
        public void TryGetT()
        {
            var composite = new MessagePackProviderComposite();
            var provider = new MessagePackProvider();
            var formatter = new Formatter(provider, MessagePackContext.Empty);

            provider.Add(formatter);
            composite.Providers.Add(provider);

            bool result0 = provider.TryGet(out IMessagePackFormatter<Target> formatter0);
            bool result1 = provider.TryGet(out IMessagePackFormatter<bool> formatter1);

            Assert.True(result0);
            Assert.False(result1);
            Assert.NotNull(formatter0);
            Assert.Null(formatter1);
        }

        [Test]
        public void TryGet()
        {
            var composite = new MessagePackProviderComposite();
            var provider = new MessagePackProvider();
            var formatter = new Formatter(provider, MessagePackContext.Empty);

            provider.Add(formatter);
            composite.Providers.Add(provider);

            bool result0 = provider.TryGet(typeof(Target), out IMessagePackFormatter formatter0);
            bool result1 = provider.TryGet(typeof(bool), out IMessagePackFormatter formatter1);

            Assert.True(result0);
            Assert.False(result1);
            Assert.NotNull(formatter0);
            Assert.Null(formatter1);
        }
    }
}
