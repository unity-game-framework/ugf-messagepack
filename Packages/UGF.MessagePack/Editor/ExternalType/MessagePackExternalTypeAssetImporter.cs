using UGF.Code.Generate.Editor.Container.External;
using UnityEditor.Experimental.AssetImporters;

namespace UGF.MessagePack.Editor.ExternalType
{
    [ScriptedImporter(0, "messagepack-external")]
    public class MessagePackExternalTypeAssetImporter : CodeGenerateContainerExternalAssetImporter<MessagePackExternalTypeInfo>
    {
    }
}
