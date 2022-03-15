using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Controllers.Utils;
using UnityEditor;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    [CustomEditor(typeof(CompositeMeshProvider))]
    [CanEditMultipleObjects]
    public class CompositeMeshProviderEditor : MeshProviderEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var compositeMeshProvider = (CompositeMeshProvider)target;
            MeshProviderMenus.Menu.PropertyField(compositeMeshProvider, ref compositeMeshProvider.meshProviders);

            MeshModifierProviderMenus.Menu.ModifierPopup(compositeMeshProvider);
        }
    }
}