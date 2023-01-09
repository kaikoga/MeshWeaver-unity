using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class ProviderEditorBase : MeshWeaverEditorBase
    {
        public sealed override VisualElement CreateInspectorGUI()
        {
            return new IMGUIContainer(OnInspectorIMGUI);
        }

        protected abstract void OnInspectorIMGUI();
    }
}