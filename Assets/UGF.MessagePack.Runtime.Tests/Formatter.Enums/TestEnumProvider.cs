using NUnit.Framework;
using UGF.MessagePack.Runtime.Formatter.Enums;

namespace UGF.MessagePack.Runtime.Tests.Formatter.Enums
{
    public class TestEnumProvider
    {
        private enum EnumByte : byte
        {
        }

        private enum EnumSByte : sbyte
        {
        }

        private enum EnumInt16 : short
        {
        }

        private enum EnumInt32
        {
        }

        private enum EnumInt64 : long
        {
        }

        private enum EnumUInt16 : ushort
        {
        }

        private enum EnumUInt32 : uint
        {
        }

        private enum EnumUInt64 : ulong
        {
        }

        [Test]
        public void TryGetT()
        {
            var provider = new EnumProvider(new MessagePackProvider(), MessagePackContext.Empty);

            bool result0 = provider.TryGet(out IMessagePackFormatter<EnumByte> formatter0);
            bool result1 = provider.TryGet(out IMessagePackFormatter<EnumSByte> formatter1);
            bool result2 = provider.TryGet(out IMessagePackFormatter<EnumInt16> formatter2);
            bool result3 = provider.TryGet(out IMessagePackFormatter<EnumInt32> formatter3);
            bool result4 = provider.TryGet(out IMessagePackFormatter<EnumInt64> formatter4);
            bool result5 = provider.TryGet(out IMessagePackFormatter<EnumUInt16> formatter5);
            bool result6 = provider.TryGet(out IMessagePackFormatter<EnumUInt32> formatter6);
            bool result7 = provider.TryGet(out IMessagePackFormatter<EnumUInt64> formatter7);

            Assert.True(result0);
            Assert.True(result1);
            Assert.True(result2);
            Assert.True(result3);
            Assert.True(result4);
            Assert.True(result5);
            Assert.True(result6);
            Assert.True(result7);
            Assert.NotNull(formatter0);
            Assert.NotNull(formatter1);
            Assert.NotNull(formatter2);
            Assert.NotNull(formatter3);
            Assert.NotNull(formatter4);
            Assert.NotNull(formatter5);
            Assert.NotNull(formatter6);
            Assert.NotNull(formatter7);
            Assert.IsAssignableFrom<EnumFormatterByte<EnumByte>>(formatter0);
            Assert.IsAssignableFrom<EnumFormatterSByte<EnumSByte>>(formatter1);
            Assert.IsAssignableFrom<EnumFormatterInt16<EnumInt16>>(formatter2);
            Assert.IsAssignableFrom<EnumFormatterInt32<EnumInt32>>(formatter3);
            Assert.IsAssignableFrom<EnumFormatterInt64<EnumInt64>>(formatter4);
            Assert.IsAssignableFrom<EnumFormatterUInt16<EnumUInt16>>(formatter5);
            Assert.IsAssignableFrom<EnumFormatterUInt32<EnumUInt32>>(formatter6);
            Assert.IsAssignableFrom<EnumFormatterUInt64<EnumUInt64>>(formatter7);
        }

        [Test]
        public void TryGet()
        {
            var provider = new EnumProvider(new MessagePackProvider(), MessagePackContext.Empty);

            bool result0 = provider.TryGet(typeof(EnumByte), out IMessagePackFormatter formatter0);
            bool result1 = provider.TryGet(typeof(EnumSByte), out IMessagePackFormatter formatter1);
            bool result2 = provider.TryGet(typeof(EnumInt16), out IMessagePackFormatter formatter2);
            bool result3 = provider.TryGet(typeof(EnumInt32), out IMessagePackFormatter formatter3);
            bool result4 = provider.TryGet(typeof(EnumInt64), out IMessagePackFormatter formatter4);
            bool result5 = provider.TryGet(typeof(EnumUInt16), out IMessagePackFormatter formatter5);
            bool result6 = provider.TryGet(typeof(EnumUInt32), out IMessagePackFormatter formatter6);
            bool result7 = provider.TryGet(typeof(EnumUInt64), out IMessagePackFormatter formatter7);

            Assert.True(result0);
            Assert.True(result1);
            Assert.True(result2);
            Assert.True(result3);
            Assert.True(result4);
            Assert.True(result5);
            Assert.True(result6);
            Assert.True(result7);
            Assert.NotNull(formatter0);
            Assert.NotNull(formatter1);
            Assert.NotNull(formatter2);
            Assert.NotNull(formatter3);
            Assert.NotNull(formatter4);
            Assert.NotNull(formatter5);
            Assert.NotNull(formatter6);
            Assert.NotNull(formatter7);
            Assert.IsAssignableFrom<EnumFormatterByte<EnumByte>>(formatter0);
            Assert.IsAssignableFrom<EnumFormatterSByte<EnumSByte>>(formatter1);
            Assert.IsAssignableFrom<EnumFormatterInt16<EnumInt16>>(formatter2);
            Assert.IsAssignableFrom<EnumFormatterInt32<EnumInt32>>(formatter3);
            Assert.IsAssignableFrom<EnumFormatterInt64<EnumInt64>>(formatter4);
            Assert.IsAssignableFrom<EnumFormatterUInt16<EnumUInt16>>(formatter5);
            Assert.IsAssignableFrom<EnumFormatterUInt32<EnumUInt32>>(formatter6);
            Assert.IsAssignableFrom<EnumFormatterUInt64<EnumUInt64>>(formatter7);
        }
    }
}
