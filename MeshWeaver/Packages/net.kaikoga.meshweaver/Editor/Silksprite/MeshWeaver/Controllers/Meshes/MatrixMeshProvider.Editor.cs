using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Commands;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.GUIActions;
using Silksprite.MeshWeaver.GUIActions.Extensions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(MatrixMeshProvider))]
    [CanEditMultipleObjects]
    public class MatrixMeshProviderEditor : MeshProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(new Header(Loc("Sources")));
            container.Add(Prop(nameof(MatrixMeshProvider.pathProviderX), Loc("MatrixMeshProvider.pathProviderX")));
            container.Add(PathProviderMenus.CollectionsMenu.ToGUIAction((MatrixMeshProvider)target, "Path X",
                serializedObject.FindProperty(nameof(MatrixMeshProvider.pathProviderX))));
            container.Add(Prop(nameof(MatrixMeshProvider.pathProviderY), Loc("MatrixMeshProvider.pathProviderY")));
            container.Add(PathProviderMenus.CollectionsMenu.ToGUIAction((MatrixMeshProvider)target, "Path Y",
                serializedObject.FindProperty(nameof(MatrixMeshProvider.pathProviderY))));
            container.Add(Prop(nameof(MatrixMeshProvider.operatorKind), Loc("MatrixMeshProvider.operatorKind")));

            container.Add(new Header(Loc("Output")));
            container.Add(Prop(nameof(MatrixMeshProvider.material), Loc("MatrixMeshProvider.material")));
            container.Add(Prop(nameof(MatrixMeshProvider.defaultCellPatternKind), Loc("MatrixMeshProvider.defaultCellPatternKind")));
            container.Add(Prop(nameof(MatrixMeshProvider.cellPatternOverrides), Loc("MatrixMeshProvider.cellPatternOverrides")));
        }

        protected override void PopulateAdvancedActions(List<LocMenuItem> menuItems)
        {
            menuItems.Add(new UpgradeBySwapScript<MatrixMeshProvider, PillarMeshProvider>().ToLocMenuItem(target as MatrixMeshProvider));
        }

        protected override void PopulateDumpGUI(GUIContainer container)
        {
            container.Add(new DumpFoldout(Loc("Input Path dump X"), () => ((MatrixMeshProvider)target).LastPathieX?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
            container.Add(new DumpFoldout(Loc("Input Path dump Y"), () => ((MatrixMeshProvider)target).LastPathieY?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
        }
    }
}