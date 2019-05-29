using NUnit.Framework;

namespace UGF.MessagePack.Runtime.Tests
{
    public class TestMessagePackProvider
    {
        private class Target
        {
        }

        private class Formatter : MessagePackFormatterBase<Target>
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
        public void Add()
        {
            var provider = new MessagePackProvider();
            var formatter = new Formatter(provider, MessagePackContext.Empty);

            provider.Add(formatter);

            Assert.AreEqual(1, provider.Formatters.Count);
            Assert.True(provider.Formatters.ContainsKey(typeof(Target)));
            Assert.True(provider.Formatters.ContainsValue(formatter));
        }

        [Test]
        public void Remove()
        {
            var provider = new MessagePackProvider();
            var formatter = new Formatter(provider, MessagePackContext.Empty);

            provider.Add(formatter);

            Assert.AreEqual(1, provider.Formatters.Count);
            Assert.True(provider.Formatters.ContainsKey(typeof(Target)));
            Assert.True(provider.Formatters.ContainsValue(formatter));

            provider.Remove<Target>();

            Assert.AreEqual(0, provider.Formatters.Count);
            Assert.False(provider.Formatters.ContainsKey(typeof(Target)));
            Assert.False(provider.Formatters.ContainsValue(formatter));
        }

        [Test]
        public void TryGetT()
        {
            var provider = new MessagePackProvider();
            var formatter = new Formatter(provider, MessagePackContext.Empty);

            provider.Add(formatter);

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
            var provider = new MessagePackProvider();
            var formatter = new Formatter(provider, MessagePackContext.Empty);

            provider.Add(formatter);

            bool result0 = provider.TryGet(typeof(Target), out IMessagePackFormatter formatter0);
            bool result1 = provider.TryGet(typeof(bool), out IMessagePackFormatter formatter1);

            Assert.True(result0);
            Assert.False(result1);
            Assert.NotNull(formatter0);
            Assert.Null(formatter1);
        }
    }
}
