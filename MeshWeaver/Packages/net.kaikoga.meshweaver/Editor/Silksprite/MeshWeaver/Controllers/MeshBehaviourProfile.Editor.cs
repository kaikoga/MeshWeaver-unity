using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MeshBehaviourProfile))]
    public class MeshBehaviourProfileEditor : MeshWeaverEditorBase
    {
        protected override bool IsMainComponentEditor => false;

        protected sealed override void PopulateInspectorGUI(GUIContainer container)
        {
            GUIAction ChildProp(string relativePath, LocalizedContent loc) => Prop($"{nameof(MeshBehaviourProfile.data)}.{relativePath}", loc);
            container.Add(new Header(Loc("Realtime Mesh Generator Settings")));
            container.Add(ChildProp(nameof(MeshBehaviourProfileData.realtimeNormalGeneratorKind), Loc("MeshBehaviourProfileData.realtimeNormalGeneratorKind")));
            container.Add(ChildProp(nameof(MeshBehaviourProfileData.realtimeNormalGeneratorAngle), Loc("MeshBehaviourProfileData.realtimeNormalGeneratorAngle")));
            container.Add(ChildProp(nameof(MeshBehaviourProfileData.realtimeLightmapGeneratorKind), Loc("MeshBehaviourProfileData.realtimeLightmapGeneratorKind")));
            
            container.Add(new Header(Loc("Exported Mesh Generator Settings")));
            container.Add(ChildProp(nameof(MeshBehaviourProfileData.exportedNormalGeneratorKind), Loc("MeshBehaviourProfileData.exportedNormalGeneratorKind")));
            container.Add(ChildProp(nameof(MeshBehaviourProfileData.exportedNormalGeneratorAngle), Loc("MeshBehaviourProfileData.exportedNormalGeneratorAngle")));
            container.Add(ChildProp(nameof(MeshBehaviourProfileData.exportedLightmapGeneratorKind), Loc("MeshBehaviourProfileData.exportedLightmapGeneratorKind")));
            
            container.Add(new Header(Loc("Exported Prefab Settings")));
            container.Add(ChildProp(nameof(MeshBehaviourProfileData.useLod), Loc("MeshBehaviourProfileData.useLod")));
            container.Add(ChildProp(nameof(MeshBehaviourProfileData.useLod1), Loc("MeshBehaviourProfileData.useLod1")));
            container.Add(ChildProp(nameof(MeshBehaviourProfileData.useLod2), Loc("MeshBehaviourProfileData.useLod2")));
            container.Add(ChildProp(nameof(MeshBehaviourProfileData.useCollider), Loc("MeshBehaviourProfileData.useCollider")));
            container.Add(ChildProp(nameof(MeshBehaviourProfileData.useLightmap), Loc("MeshBehaviourProfileData.useLightmap")));
            container.Add(ChildProp("staticEditorFlags", Loc("MeshBehaviourProfileData.staticEditorFlags")));
        }
    }
}