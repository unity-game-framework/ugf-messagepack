using System;
using UGF.Assemblies.Runtime;

namespace UGF.MessagePack.Runtime
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MessagePackFormatterAttribute : AssemblyBrowsableTypeAttribute
    {
        public Type TargetType { get; }

        public MessagePackFormatterAttribute(Type targetType)
        {
            TargetType = targetType;
        }
    }
}
