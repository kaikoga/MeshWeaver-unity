using Silksprite.MeshWeaver.Controllers.Base;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(ImportMeshProvider))]
    [CanEditMultipleObjects]
    public class ImportMeshProviderEditor : MeshProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(ImportMeshProvider.mesh), Loc("ImportMeshProvider.mesh")));
            container.Add(Prop(nameof(ImportMeshProvider.materials), Loc("ImportMeshProvider.materials")));
        }
    }
}