using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Fallback;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.UIElements;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(PillarMeshProvider))]
    [CanEditMultipleObjects]
    public class PillarMeshProviderEditor : MeshProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(PillarMeshProvider.fillBody), Loc("PillarMeshProvider.fillBody")));
            container.Add(Prop(nameof(PillarMeshProvider.fillBottom), Loc("PillarMeshProvider.fillBottom")));
            container.Add(Prop(nameof(PillarMeshProvider.fillTop), Loc("PillarMeshProvider.fillTop")));

            container.Add(Prop(nameof(PillarMeshProvider.uvChannelBody), Loc("PillarMeshProvider.uvChannelBody")));
            container.Add(Prop(nameof(PillarMeshProvider.uvChannelBottom), Loc("PillarMeshProvider.uvChannelBottom")));
            container.Add(Prop(nameof(PillarMeshProvider.uvChannelTop), Loc("PillarMeshProvider.uvChannelTop")));

            container.Add(Prop(nameof(PillarMeshProvider.materialBody), Loc("PillarMeshProvider.materialBody")));
            container.Add(Prop(nameof(PillarMeshProvider.materialBottom), Loc("PillarMeshProvider.materialBottom")));
            container.Add(Prop(nameof(PillarMeshProvider.materialTop), Loc("PillarMeshProvider.materialTop")));

            container.Add(Prop(nameof(PillarMeshProvider.pathProviderX), Loc("PillarMeshProvider.pathProviderX")));
            container.Add(PathProviderMenus.CollectionsMenu.VisualElement((PillarMeshProvider)target, "Path X",
                serializedObject.FindProperty(nameof(PillarMeshProvider.pathProviderX))));
            container.Add(Prop(nameof(PillarMeshProvider.pathProviderY), Loc("PillarMeshProvider.pathProviderY")));
            container.Add(PathProviderMenus.CollectionsMenu.VisualElement((PillarMeshProvider)target, "Path Y",
                serializedObject.FindProperty(nameof(PillarMeshProvider.pathProviderY))));

            container.Add(Prop(nameof(PillarMeshProvider.operatorKind), Loc("PillarMeshProvider.operatorKind")));
            container.Add(Prop(nameof(PillarMeshProvider.defaultCellPatternKind), Loc("PillarMeshProvider.defaultCellPatternKind")));
            container.Add(Prop(nameof(PillarMeshProvider.cellPatternOverrides), Loc("PillarMeshProvider.cellPatternOverrides")));

            container.Add(Prop(nameof(PillarMeshProvider.longitudeAxisKind), Loc("PillarMeshProvider.longitudeAxisKind")));
            container.Add(Prop(nameof(PillarMeshProvider.reverseLids), Loc("PillarMeshProvider.reverseLids")));
        }

        protected override void PopulateDumpGUI(VisualElement container)
        {
            var pillarMeshProvider = (PillarMeshProvider)target;
            container.Add(new DumpFoldout(Loc("Input Path dump X"), () => pillarMeshProvider.LastPathieX?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
            container.Add(new DumpFoldout(Loc("Input Path dump Y"), () => pillarMeshProvider.LastPathieY?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
        }
    }
}