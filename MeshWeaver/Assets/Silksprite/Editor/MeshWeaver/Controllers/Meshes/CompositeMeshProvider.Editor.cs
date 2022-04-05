using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(CompositeMeshProvider))]
    [CanEditMultipleObjects]
    public class CompositeMeshProviderEditor : MeshProviderEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var compositeMeshProvider = (CompositeMeshProvider)target;
            MeshProviderMenus.Menu.ChildPopup(compositeMeshProvider);
        }
    }
}