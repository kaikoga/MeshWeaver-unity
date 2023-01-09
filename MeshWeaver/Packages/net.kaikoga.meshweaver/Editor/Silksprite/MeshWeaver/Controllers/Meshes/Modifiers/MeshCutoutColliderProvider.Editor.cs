using Silksprite.MeshWeaver.Controllers.Fallback;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    [CustomEditor(typeof(MeshCutoutColliderProvider))]
    [CanEditMultipleObjects]
    public class MeshCutoutColliderProviderEditor : SubProviderBaseEditor
    {
        protected override void OnPropertiesGUI()
        {
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