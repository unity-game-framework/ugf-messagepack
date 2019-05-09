using System;
using UGF.Code.Generate.Editor.Container.External;
using UnityEngine;

namespace UGF.MessagePack.Editor.ExternalType
{
    [Serializable]
    public class MessagePackExternalTypeMemberInfo : CodeGenerateContainerExternalMemberInfo
    {
        [SerializeField] private int m_intKey;
        [SerializeField] private string m_stringKey;

        public int IntKey { get { return m_intKey; } set { m_intKey = value; } }
        public string StringKey { get { return m_stringKey; } set { m_stringKey = value; } }
    }
}
