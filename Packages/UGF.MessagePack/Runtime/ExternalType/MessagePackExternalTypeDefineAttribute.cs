using System;
using JetBrains.Annotations;
using UGF.Assemblies.Runtime;

namespace UGF.MessagePack.Runtime.ExternalType
{
    [BaseTypeRequired(typeof(IMessagePackExternalTypeDefine))]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MessagePackExternalTypeDefineAttribute : AssemblyBrowsableTypeAttribute
    {
    }
}
