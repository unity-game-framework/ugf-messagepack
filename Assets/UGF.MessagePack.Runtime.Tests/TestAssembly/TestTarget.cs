using System;

namespace UGF.MessagePack.Runtime.Tests.TestAssembly
{
    public class TestTarget
    {
        public bool BoolValue { get; set; } = true;
        public int IntValue { get; set; } = 10;
        public float FloatValue { get; set; } = 10F;
        public string StringValue { get; set; } = "value";
        public TypeCode EnumValue { get; set; } = TypeCode.Boolean;
    }
}
