using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class SubProviderEditorBase : ProviderEditorBase
    {
        protected override bool IsMainComponentEditor => false;

        protected sealed override void PopulateInspectorGUI(VisualElement container)
        {
            PopulatePropertiesGUI(container);
        }

        protected abstract void PopulatePropertiesGUI(VisualElement container);
    }
}