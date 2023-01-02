using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(CompositeMeshProvider))]
    [CanEditMultipleObjects]
    public class CompositeMeshProviderEditor : MeshProviderEditor
    {
        protected override void OnPropertiesGUI()
        {
            base.OnPropertiesGUI();
            var compositeMeshProvider = (CompositeMeshProvider)target;
            MeshProviderMenus.Menu.ChildPopup(compositeMeshProvider, "Mesh Providers");
        }
    }
}