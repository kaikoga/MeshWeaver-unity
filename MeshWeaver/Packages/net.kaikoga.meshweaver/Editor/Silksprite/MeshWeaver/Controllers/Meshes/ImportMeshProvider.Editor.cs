using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(ImportMeshProvider))]
    [CanEditMultipleObjects]
    public class ImportMeshProviderEditor : MeshProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(ImportMeshProvider.mesh), Loc("ImportMeshProvider.mesh")));
            container.Add(Prop(nameof(ImportMeshProvider.materials), Loc("ImportMeshProvider.materials")));
        }
    }
}