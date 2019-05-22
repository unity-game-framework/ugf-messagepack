using System;
using MessagePack;
using NUnit.Framework;
using UnityEngine.Profiling;

namespace UGF.MessagePack.Runtime.Tests
{
    public class TestMessagePackUtility
    {
        [Test]
        public void CastToInt()
        {
            var typeCode = TypeCode.Byte;

            Profiler.BeginSample("Test.CastToInt");

            int value = MessagePackUnsafeUtility.As<TypeCode, int>(ref typeCode);

            Profiler.EndSample();

            Assert.AreEqual(6, value);
        }

        [Test]
        public void CastToTypeCode()
        {
            int value = 6;

            Profiler.BeginSample("Test.CastToTypeCode");

            TypeCode typeCode = MessagePackUnsafeUtility.As<int, TypeCode>(ref value);

            Profiler.EndSample();

            Assert.AreEqual(TypeCode.Byte, typeCode);
        }
    }
}
