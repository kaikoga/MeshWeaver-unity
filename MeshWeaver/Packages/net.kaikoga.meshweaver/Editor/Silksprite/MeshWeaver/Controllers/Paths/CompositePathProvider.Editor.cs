using Silksprite.MeshWeaver.Controllers.Fallback;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    [CustomEditor(typeof(CompositePathProvider))]
    [CanEditMultipleObjects]
    public class CompositePathProviderEditor : PathProviderEditor
    {
        protected override void OnPropertiesGUI()
        {
            var compositePathProvider = (CompositePathProvider)target;
            PathProviderMenus.ElementsMenu.ChildPopup(compositePathProvider, Tr("Path Providers"));
        }
    }
}