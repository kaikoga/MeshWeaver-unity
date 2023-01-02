using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;
using UnityEngine;

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
            Collider predicate = null;
            ColliderMenus.Menu.PropertyField(meshCutoutColliderProvider, "Predicate", "Predicate", ref predicate);
            if (predicate)
            {
                meshCutoutColliderProvider.predicates.Add(predicate);
            }
        }
    }
}