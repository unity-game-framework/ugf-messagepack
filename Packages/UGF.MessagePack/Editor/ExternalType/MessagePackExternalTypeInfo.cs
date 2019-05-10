using System;
using UGF.Code.Generate.Editor.Container.External;
using UnityEngine;

namespace UGF.MessagePack.Editor.ExternalType
{
    [Serializable]
    public class MessagePackExternalTypeInfo : CodeGenerateContainerExternalInfoBase<MessagePackExternalTypeMemberInfo>
    {
        [SerializeField] private bool m_keyAsPropertyName;

        public bool KeyAsPropertyName { get { return m_keyAsPropertyName; } set { m_keyAsPropertyName = value; } }
    }
}
