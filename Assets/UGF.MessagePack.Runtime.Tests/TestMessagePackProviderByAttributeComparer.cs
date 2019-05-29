using System.Collections.Generic;
using NUnit.Framework;

namespace UGF.MessagePack.Runtime.Tests
{
    public class TestMessagePackProviderByAttributeComparer
    {
        [MessagePackProvider]
        private class Provider0 : MessagePackProvider
        {
        }

        [MessagePackProvider(5)]
        private class Provider1 : MessagePackProvider
        {
        }

        [MessagePackProvider(-5)]
        private class Provider2 : MessagePackProvider
        {
        }

        [Test]
        public void Sort()
        {
            var list = new List<IMessagePackProvider>
            {
                new Provider0(),
                new Provider1(),
                new Provider2()
            };

            list.Sort(MessagePackProviderByAttributeComparer.Default);

            Assert.IsAssignableFrom<Provider2>(list[0]);
            Assert.IsAssignableFrom<Provider0>(list[1]);
            Assert.IsAssignableFrom<Provider1>(list[2]);
        }
    }
}
