using UnityEditor;
using UnityEngine;

namespace UGF.MessagePack.Editor.ExternalType
{
    internal static class MessagePackExternalTypeEditorMenu
    {
        [MenuItem("Assets/Create/UGF/MessagePack/MessagePack External Type", false, 2000)]
        private static void ExternalTypeCreateMenu()
        {
            Texture2D icon = AssetPreview.GetMiniTypeThumbnail(typeof(TextAsset));
            string extension = MessagePackExternalTypeEditorUtility.ExternalTypeAssetExtension;

            ProjectWindowUtil.CreateAssetWithContent($"New MessagePack External Type.{extension}", "{}", icon);
        }
    }
}
