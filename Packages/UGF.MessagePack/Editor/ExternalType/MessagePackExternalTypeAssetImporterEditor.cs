using UGF.Code.Generate.Editor.Container.External;
using UnityEditor;
using UnityEngine;

namespace UGF.MessagePack.Editor.ExternalType
{
    [CustomEditor(typeof(MessagePackExternalTypeAssetImporter))]
    public class MessagePackExternalTypeAssetImporterEditor : CodeGenerateContainerExternalAssetImporterEditor
    {
        private SerializedProperty m_propertyKeyAsPropertyName;

        public override void OnEnable()
        {
            base.OnEnable();

            m_propertyKeyAsPropertyName = InfoSerializedProperty.FindPropertyRelative("m_keyAsPropertyName");
        }

        protected override void OnDrawMembers(SerializedProperty propertyMembers)
        {
            DrawOptions();

            base.OnDrawMembers(propertyMembers);
        }

        protected override void OnDrawMember(SerializedProperty propertyMembers, int index)
        {
            SerializedProperty propertyMember = propertyMembers.GetArrayElementAtIndex(index);
            SerializedProperty propertyName = propertyMember.FindPropertyRelative("m_name");
            SerializedProperty propertyActive = propertyMember.FindPropertyRelative("m_active");
            SerializedProperty propertyField = m_propertyKeyAsPropertyName.boolValue
                ? propertyMember.FindPropertyRelative("m_stringKey")
                : propertyMember.FindPropertyRelative("m_intKey");

            Rect rect = EditorGUILayout.GetControlRect();
            Rect rectContent = EditorGUI.PrefixLabel(rect, new GUIContent(propertyName.stringValue));
            var rectToggle = new Rect(rectContent.x, rectContent.y, 16F, rectContent.height);
            var rectField = new Rect(rectContent.x + rectToggle.width + 5F, rectContent.y, rectContent.width - rectToggle.width - 5F, rectContent.height);

            propertyActive.boolValue = EditorGUI.ToggleLeft(rectToggle, GUIContent.none, propertyActive.boolValue);

            EditorGUI.PropertyField(rectField, propertyField, GUIContent.none);
        }

        protected override void OnTypeChangedSetupDefaultMemberInfo(SerializedProperty propertyMembers, SerializedProperty propertyMember, CodeGenerateContainerExternalMemberInfo memberInfo, int index)
        {
            base.OnTypeChangedSetupDefaultMemberInfo(propertyMembers, propertyMember, memberInfo, index);

            SerializedProperty propertyIntKey = propertyMember.FindPropertyRelative("m_intKey");
            SerializedProperty propertyStringKey = propertyMember.FindPropertyRelative("m_stringKey");

            propertyIntKey.intValue = index;
            propertyStringKey.stringValue = memberInfo.Name;
        }

        private void DrawOptions()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Options", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(m_propertyKeyAsPropertyName);
        }
    }
}
