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

        [Key(1)]
        public bool BoolValue { get; set; } = true;

        [Key(2)]
        public float FloatValue { get; set; } = 50.5F;

        [Key(3)]
        public int IntValue { get; set; } = 50;

        [Key(4)]
        public Vector2 Vector2 { get; set; } = Vector2.one;

        [Key(5)]
        public Bounds Bounds { get; set; } = new Bounds(Vector3.one, Vector3.one);

        [Key(6)]
        public HideFlags Flags { get; set; } = HideFlags.DontSave;

        public int ReadOnly { get; } = 10;
    }
}
