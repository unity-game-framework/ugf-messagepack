using MessagePack;
using UnityEngine;

namespace UGF.MessagePack.Runtime.Tests.TestAssembly
{
    [MessagePackSerializable]
    [MessagePackObject]
    public class TestTarget
    {
        [Key(0)]
        public string Name { get; set; } = "TestTarget";
        public bool BoolValue { get; set; } = true;
        public float FloatValue { get; set; } = 50.5F;
        public int IntValue { get; set; } = 50;
        public Vector2 Vector2 { get; set; } = Vector2.one;
        public Bounds Bounds { get; set; } = new Bounds(Vector3.one, Vector3.one);
        public HideFlags Flags { get; set; } = HideFlags.DontSave;
        public int ReadOnly { get; } = 10;
    }
}
