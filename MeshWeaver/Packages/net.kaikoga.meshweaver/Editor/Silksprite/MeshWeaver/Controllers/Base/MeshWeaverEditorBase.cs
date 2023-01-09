using Silksprite.MeshWeaver.Scopes;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Utils.Localization;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class MeshWeaverEditorBase : Editor
    {
        public sealed override VisualElement CreateInspectorGUI()
        {
            return new IMGUIContainer(OnInspectorIMGUI);
        }

        public sealed override void OnInspectorGUI() { }

        protected abstract void OnInspectorIMGUI();

        protected void OnBaseInspectorGUI()
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