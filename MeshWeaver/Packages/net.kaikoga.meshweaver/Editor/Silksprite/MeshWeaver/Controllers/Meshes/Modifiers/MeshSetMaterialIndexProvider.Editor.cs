using Silksprite.MeshWeaver.Controllers.Base;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    [CustomEditor(typeof(MeshSetMaterialIndexProvider))]
    [CanEditMultipleObjects]
    public class MeshSetMaterialIndexProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(MeshSetMaterialIndexProvider.materialIndex), Loc("MeshSetMaterialIndexProvider.materialIndex")));
        }
    }
}