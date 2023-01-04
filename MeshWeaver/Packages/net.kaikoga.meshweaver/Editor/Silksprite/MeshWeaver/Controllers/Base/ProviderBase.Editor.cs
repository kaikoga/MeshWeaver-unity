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
                MeshWeaverGUI.Header(Tr("Fallback Inspector"));
                base.OnInspectorGUI();
                MeshWeaverGUI.Header(Tr("End Fallback Inspector"));
            }
        }
    }
}