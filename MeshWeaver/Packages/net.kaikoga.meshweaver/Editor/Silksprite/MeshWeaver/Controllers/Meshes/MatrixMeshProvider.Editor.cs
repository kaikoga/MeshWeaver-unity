using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.UIElements;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(MatrixMeshProvider))]
    [CanEditMultipleObjects]
    public class MatrixMeshProviderEditor : MeshProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(MatrixMeshProvider.pathProviderX), Loc("MatrixMeshProvider.pathProviderX")));
            container.Add(PathProviderMenus.CollectionsMenu.VisualElement((MatrixMeshProvider)target, " ", "Path X",
                serializedObject.FindProperty(nameof(MatrixMeshProvider.pathProviderX))));
            container.Add(Prop(nameof(MatrixMeshProvider.pathProviderY), Loc("MatrixMeshProvider.pathProviderY")));
            container.Add(PathProviderMenus.CollectionsMenu.VisualElement((MatrixMeshProvider)target, " ", "Path Y",
                serializedObject.FindProperty(nameof(MatrixMeshProvider.pathProviderY))));
            container.Add(Prop(nameof(MatrixMeshProvider.operatorKind), Loc("MatrixMeshProvider.operatorKind")));
            container.Add(Prop(nameof(MatrixMeshProvider.defaultCellPatternKind), Loc("MatrixMeshProvider.defaultCellPatternKind")));
            container.Add(Prop(nameof(MatrixMeshProvider.cellPatternOverrides), Loc("MatrixMeshProvider.cellPatternOverrides")));
            container.Add(Prop(nameof(MatrixMeshProvider.material), Loc("MatrixMeshProvider.material")));
        }

        protected override void PopulateDumpGUI(VisualElement container)
        {
            container.Add(new DumpFoldout(Loc("Path data X"), () => ((MatrixMeshProvider)target).LastPathieX?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
            container.Add(new DumpFoldout(Loc("Path data Y"), () => ((MatrixMeshProvider)target).LastPathieY?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
        }
    }
}