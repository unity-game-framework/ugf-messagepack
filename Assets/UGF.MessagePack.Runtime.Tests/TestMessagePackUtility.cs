using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace UGF.MessagePack.Runtime.Tests
{
    public class TestMessagePackUtility
    {
        [MessagePackProvider(0, -1)]
        private class Provider0 : MessagePackProvider
        {
        }

        [MessagePackFormatter(-1)]
        private class Formatter : MessagePackFormatterBase<bool>
        {
            public Formatter(IMessagePackProvider provider, IMessagePackContext context) : base(provider, context)
            {
            }

            public override void Serialize(ref MessagePackWriter writer, bool value)
            {
            }

            public override bool Deserialize(ref MessagePackReader reader)
            {
                return false;
            }
        }

        [Test]
        public void CreateProvider()
        {
            var provider = MessagePackUtility.CreateProvider(MessagePackContext.Empty, -1) as MessagePackProviderComposite;

            Assert.NotNull(provider);
            Assert.AreEqual(1, provider.Formatters.Count);
            Assert.AreEqual(4, provider.Providers.Count);
        }

        [Test]
        public void GetFormatters()
        {
            var provider = new MessagePackProvider();
            var formatters = new Dictionary<Type, IMessagePackFormatter>();

            MessagePackUtility.GetFormatters(provider, MessagePackContext.Empty, formatters, -1);

            Assert.AreEqual(1, formatters.Count);

            bool result = formatters.TryGetValue(typeof(bool), out IMessagePackFormatter formatter);

            Assert.True(result);
            Assert.NotNull(formatter);
        }

        [Test]
        public void GetProviders()
        {
            var providers = new List<IMessagePackProvider>();

            MessagePackUtility.GetProviders(providers, -1);

            Assert.AreEqual(1, providers.Count);
        }
    }
}
