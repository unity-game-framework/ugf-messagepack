using System;

namespace UGF.MessagePack.Runtime
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class MessagePackSerializableAttribute : Attribute
    {
    }
}
