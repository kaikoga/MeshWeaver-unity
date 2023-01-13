using Silksprite.MeshWeaver.GUIActions;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class SubProviderEditorBase : ProviderEditorBase
    {
        protected override bool IsMainComponentEditor => false;

        protected sealed override void PopulateInspectorGUI(GUIContainer container)
        {
            PopulatePropertiesGUI(container);
        }

        protected abstract void PopulatePropertiesGUI(GUIContainer container);
    }
}