using Silksprite.MeshWeaver.Controllers.Fallback;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    [CustomEditor(typeof(MeshCutoutColliderProvider))]
    [CanEditMultipleObjects]
    public class MeshCutoutColliderProviderEditor : SubProviderBaseEditor
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(ColliderMenus.Menu.VisualElement((MeshCutoutColliderProvider)target, "Predicate", "Predicate",
                serializedObject.FindProperty(nameof(MeshCutoutColliderProvider.predicates))));
        }
    }
}