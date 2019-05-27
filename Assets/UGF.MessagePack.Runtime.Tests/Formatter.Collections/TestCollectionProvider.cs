using System.Collections.Generic;
using NUnit.Framework;
using UGF.MessagePack.Runtime.Formatter.Collections;

namespace UGF.MessagePack.Runtime.Tests.Formatter.Collections
{
    public class TestCollectionProvider
    {
        [Test]
        public void TryGetT()
        {
            Assert.Ignore();
        }

        [Test]
        public void TryGet()
        {
            var provider = new CollectionProvider(new MessagePackProvider(), MessagePackContext.Empty);

            bool result0 = provider.TryGet(typeof(int[]), out IMessagePackFormatter formatter0);
            bool result1 = provider.TryGet(typeof(List<int>), out IMessagePackFormatter formatter1);
            bool result2 = provider.TryGet(typeof(Dictionary<int, int>), out IMessagePackFormatter formatter2);

            Assert.True(result0);
            Assert.True(result1);
            Assert.True(result2);
            Assert.NotNull(formatter0);
            Assert.NotNull(formatter1);
            Assert.NotNull(formatter2);
        }
    }
}
