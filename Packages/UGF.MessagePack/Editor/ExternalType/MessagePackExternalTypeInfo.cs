using System;
using UGF.Code.Generate.Editor.Container.External;

namespace UGF.MessagePack.Editor.ExternalType
{
    [Serializable]
    public class MessagePackExternalTypeInfo : CodeGenerateContainerExternalInfoBase<CodeGenerateContainerExternalMemberInfo>
    {
        public bool IsValid()
        {
            return TryGetTargetType(out _);
        }
    }
}
