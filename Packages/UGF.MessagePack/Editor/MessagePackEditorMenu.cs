using UnityEditor;
using UnityEditorInternal;

namespace UGF.MessagePack.Editor
{
    internal static class MessagePackEditorMenu
    {
        [MenuItem("CONTEXT/AssemblyDefinitionImporter/MessagePack Generate Formatters", false, 1000)]
        private static void AssemblyGenerateFormattersMenu(MenuCommand menuCommand)
        {
            var importer = (AssemblyDefinitionImporter)menuCommand.context;

            MessagePackEditorUtility.GenerateAssetFromAssembly(importer.assetPath);
        }
    }
}
