using System;
using MessagePack;
using NUnit.Framework;
using UnityEngine.Profiling;

namespace UGF.MessagePack.Runtime.Tests
{
    public class TestMessagePackUnsafeUtility
    {
        [Test]
        public void AsFromTypeCodeToInt32()
        {
            var typeCode = TypeCode.Byte;

            Profiler.BeginSample("Test.AsFromTypeCodeToInt32");

            int value = MessagePackUnsafeUtility.As<TypeCode, int>(ref typeCode);

            Profiler.EndSample();

            Assert.AreEqual(6, value);
        }

        [Test]
        public void AsFromInt32ToTypeCode()
        {
            int value = 6;

            Profiler.BeginSample("Test.AsFromInt32ToTypeCode");

            TypeCode typeCode = MessagePackUnsafeUtility.As<int, TypeCode>(ref value);

            Profiler.EndSample();

            Assert.AreEqual(TypeCode.Byte, typeCode);
        }
    }
}
