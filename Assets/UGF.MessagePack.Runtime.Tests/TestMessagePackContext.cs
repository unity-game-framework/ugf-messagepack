using NUnit.Framework;

namespace UGF.MessagePack.Runtime.Tests
{
    public class TestMessagePackContext
    {
        private class Target
        {
        }

        [Test]
        public void GetT()
        {
            var context = new MessagePackContext
            {
                Values =
                {
                    { typeof(Target), new Target() }
                }
            };

            var target = context.Get<Target>();

            Assert.NotNull(target);
        }

        [Test]
        public void Get()
        {
            var context = new MessagePackContext
            {
                Values =
                {
                    { typeof(Target), new Target() }
                }
            };

            object target = context.Get(typeof(Target));

            Assert.NotNull(target);
        }

        [Test]
        public void TryGetT()
        {
            var context = new MessagePackContext
            {
                Values =
                {
                    { typeof(Target), new Target() }
                }
            };

            bool result = context.TryGet(out Target target);

            Assert.True(result);
            Assert.NotNull(target);
        }

        [Test]
        public void TryGet()
        {
            var context = new MessagePackContext
            {
                Values =
                {
                    { typeof(Target), new Target() }
                }
            };

            bool result = context.TryGet(typeof(Target), out object target);

            Assert.True(result);
            Assert.NotNull(target);
        }
    }
}
