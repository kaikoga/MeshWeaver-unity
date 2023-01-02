using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    [CustomEditor(typeof(CompositePathProvider))]
    [CanEditMultipleObjects]
    public class CompositePathProviderEditor : PathProviderEditor
    {
        protected override void OnPropertiesGUI()
        {
            base.OnPropertiesGUI();
            var compositePathProvider = (CompositePathProvider)target;
            PathProviderMenus.ElementsMenu.ChildPopup(compositePathProvider, "Path Providers");
        }
    }
}