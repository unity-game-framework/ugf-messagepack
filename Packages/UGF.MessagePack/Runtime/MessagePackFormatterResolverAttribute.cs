using System;
using JetBrains.Annotations;
using MessagePack;
using UGF.Assemblies.Runtime;

namespace UGF.MessagePack.Runtime
{
    [BaseTypeRequired(typeof(IFormatterResolver))]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MessagePackFormatterResolverAttribute : AssemblyBrowsableTypeAttribute
    {
        public int Priority { get; }

        public MessagePackFormatterResolverAttribute(int priority = 0)
        {
            Priority = priority;
        }
    }
}
