using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    [CustomEditor(typeof(CompositePathProvider))]
    [CanEditMultipleObjects]
    public class CompositePathProviderEditor : PathProviderEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var compositePathProvider = (CompositePathProvider)target;
            PathProviderMenus.ElementsMenu.ChildPopup(compositePathProvider, "Path Providers");
        }
    }
}