using Silksprite.MeshWeaver.Utils;
using UnityEngine;

namespace Silksprite.MeshWeaver.GUIActions
{
    public class Header : GUIAction
    {
        readonly LocalizedContent _loc;

        public Header(LocalizedContent loc)
        {
            _loc = loc;
        }
        
        public override void OnGUI()
        {
            GUILayout.Label(_loc.GUIContent, MeshWeaverSkin.Header);
        }
    }
}