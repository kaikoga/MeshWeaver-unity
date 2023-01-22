using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.GUIActions;
using Silksprite.MeshWeaver.GUIActions.Extensions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(PillarMeshProvider))]
    [CanEditMultipleObjects]
    public class PillarMeshProviderEditor : MeshProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(new Header(Loc("Sources")));
            container.Add(Prop(nameof(PillarMeshProvider.pathProviderX), Loc("PillarMeshProvider.pathProviderX")));
            container.Add(PathProviderMenus.CollectionsMenu.ToGUIAction((PillarMeshProvider)target, "Path X",
                serializedObject.FindProperty(nameof(PillarMeshProvider.pathProviderX))));
            container.Add(Prop(nameof(PillarMeshProvider.pathProviderY), Loc("PillarMeshProvider.pathProviderY")));
            container.Add(PathProviderMenus.CollectionsMenu.ToGUIAction((PillarMeshProvider)target, "Path Y",
                serializedObject.FindProperty(nameof(PillarMeshProvider.pathProviderY))));

            container.Add(Prop(nameof(PillarMeshProvider.operatorKind), Loc("PillarMeshProvider.operatorKind")));

            container.Add(new Header(Loc("Body")));
            container.Add(Prop(nameof(PillarMeshProvider.fillBody), Loc("PillarMeshProvider.fillBody")));
            container.Add(new Div(c =>
            {
                c.Add(Prop(nameof(PillarMeshProvider.materialBody), Loc("PillarMeshProvider.materialBody")));
                c.Add(Prop(nameof(PillarMeshProvider.uvChannelBody), Loc("PillarMeshProvider.uvChannelBody")));
                c.Add(Prop(nameof(PillarMeshProvider.defaultCellPatternKind), Loc("PillarMeshProvider.defaultCellPatternKind")));
                c.Add(Prop(nameof(PillarMeshProvider.cellPatternOverrides), Loc("PillarMeshProvider.cellPatternOverrides")));
            }).WithDisplayOnRefresh(null, () => ((PillarMeshProvider)target).fillBody).WithIndent());

            container.Add(new Header(Loc("Lids")));
            container.Add(Prop(nameof(PillarMeshProvider.longitudeAxisKind), Loc("PillarMeshProvider.longitudeAxisKind")));
            container.Add(Prop(nameof(PillarMeshProvider.fillBottom), Loc("PillarMeshProvider.fillBottom")));
            container.Add(new Div(c =>
            {
                c.Add(Prop(nameof(PillarMeshProvider.materialBottom), Loc("PillarMeshProvider.materialBottom")));
                c.Add(Prop(nameof(PillarMeshProvider.uvChannelBottom), Loc("PillarMeshProvider.uvChannelBottom")));
            }).WithDisplayOnRefresh(null, () => ((PillarMeshProvider)target).fillBottom).WithIndent());

            container.Add(Prop(nameof(PillarMeshProvider.fillTop), Loc("PillarMeshProvider.fillTop")));
            container.Add(new Div(c =>
            {
                c.Add(Prop(nameof(PillarMeshProvider.materialTop), Loc("PillarMeshProvider.materialTop")));
                c.Add(Prop(nameof(PillarMeshProvider.uvChannelTop), Loc("PillarMeshProvider.uvChannelTop")));
            }).WithDisplayOnRefresh(null, () => ((PillarMeshProvider)target).fillTop).WithIndent());

            container.Add(Prop(nameof(PillarMeshProvider.reverseLids), Loc("PillarMeshProvider.reverseLids")));


        }

        protected override void PopulateDumpGUI(GUIContainer container)
        {
            var pillarMeshProvider = (PillarMeshProvider)target;
            container.Add(new DumpFoldout(Loc("Input Path dump X"), () => pillarMeshProvider.LastPathieX?.Build(MeshWeaverSettings.Current.activeLodMaskLayer)));
            container.Add(new DumpFoldout(Loc("Input Path dump Y"), () => pillarMeshProvider.LastPathieY?.Build(MeshWeaverSettings.Current.activeLodMaskLayer)));
        }
    }
}