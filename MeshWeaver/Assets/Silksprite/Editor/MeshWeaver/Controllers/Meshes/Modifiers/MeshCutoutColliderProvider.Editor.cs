using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    [CustomEditor(typeof(MeshCutoutColliderProvider))]
    [CanEditMultipleObjects]
    public class MeshCutoutColliderProviderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var meshCutoutColliderProvider = (MeshCutoutColliderProvider)target;
            ColliderMenus.Menu.PropertyField(meshCutoutColliderProvider, "Predicate", ref meshCutoutColliderProvider.predicate);
        }
    }
}