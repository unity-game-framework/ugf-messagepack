using System;
using UGF.Assemblies.Runtime;

namespace UGF.MessagePack.Runtime
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MessagePackSerializableAttribute : AssemblyBrowsableTypeAttribute
    {
    }
}
