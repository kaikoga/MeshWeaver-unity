using Silksprite.MeshWeaver.Utils;
using UnityEditor;

namespace Silksprite.MeshWeaver.GUIActions
{
    public class LocHelpBox : GUIAction
    {
        readonly LocalizedContent _message;
        readonly MessageType _type;

        public LocHelpBox(LocalizedContent message, MessageType type)
        {
            _message = message;
            _type = type;
        }

        public override void OnGUI()
        {
            var message = _message.Tr;
            if (Localization.Lang == "ja")
            {
                message = message.Replace(" ", " ");
            }
            EditorGUILayout.HelpBox(message, _type);
        }
    }
}