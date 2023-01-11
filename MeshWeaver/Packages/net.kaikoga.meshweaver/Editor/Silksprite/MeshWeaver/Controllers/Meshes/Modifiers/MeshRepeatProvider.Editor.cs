using Silksprite.MeshWeaver.Controllers.Fallback;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    [CustomEditor(typeof(MeshRepeatProvider))]
    [CanEditMultipleObjects]
    public class MeshRepeatProviderEditor : SubProviderBaseEditor
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(TransformMenus.Menu.VisualElement((MeshRepeatProvider)target, "Offset By Reference",
                serializedObject.FindProperty(nameof(MeshRepeatProvider.offsetByReference))));
        }
    }
}