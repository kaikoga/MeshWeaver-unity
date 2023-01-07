using Silksprite.MeshWeaver.Scopes;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;
using static Silksprite.MeshWeaver.Utils.Localization;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class ProviderBaseEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            using (new BackgroundColorScope(Color.magenta))
            {
                MeshWeaverGUILayout.Header(Loc("Fallback Inspector"));
                base.OnInspectorGUI();
                MeshWeaverGUILayout.Header(Loc("End Fallback Inspector"));
            }
        }
    }
}