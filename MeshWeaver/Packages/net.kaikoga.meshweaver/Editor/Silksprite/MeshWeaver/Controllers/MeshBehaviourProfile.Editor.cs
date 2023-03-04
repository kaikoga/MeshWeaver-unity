using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using Silksprite.MeshWeaver.GUIActions.Extensions;
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
            container.Add(new Div(c =>
            {
                c.Add(new Header(Loc("Realtime Mesh Generator Settings")));
                c.Add(ChildProp(nameof(MeshBehaviourProfileData.realtimeNormalGeneratorKind), Loc("MeshBehaviourProfileData.realtimeNormalGeneratorKind")));
                c.Add(ChildProp(nameof(MeshBehaviourProfileData.realtimeNormalGeneratorAngle), Loc("MeshBehaviourProfileData.realtimeNormalGeneratorAngle")));
                c.Add(ChildProp(nameof(MeshBehaviourProfileData.realtimeLightmapGeneratorKind), Loc("MeshBehaviourProfileData.realtimeLightmapGeneratorKind")));
            
                c.Add(new Header(Loc("Exported Mesh Generator Settings")));
                c.Add(ChildProp(nameof(MeshBehaviourProfileData.exportedNormalGeneratorKind), Loc("MeshBehaviourProfileData.exportedNormalGeneratorKind")));
                c.Add(ChildProp(nameof(MeshBehaviourProfileData.exportedNormalGeneratorAngle), Loc("MeshBehaviourProfileData.exportedNormalGeneratorAngle")));
                c.Add(ChildProp(nameof(MeshBehaviourProfileData.exportedLightmapGeneratorKind), Loc("MeshBehaviourProfileData.exportedLightmapGeneratorKind")));
            
                c.Add(new Header(Loc("Exported Prefab Settings")));
                c.Add(ChildProp(nameof(MeshBehaviourProfileData.useLod), Loc("MeshBehaviourProfileData.useLod")));
                c.Add(ChildProp(nameof(MeshBehaviourProfileData.useLod1), Loc("MeshBehaviourProfileData.useLod1")));
                c.Add(ChildProp(nameof(MeshBehaviourProfileData.useLod2), Loc("MeshBehaviourProfileData.useLod2")));
                c.Add(ChildProp(nameof(MeshBehaviourProfileData.useCollider), Loc("MeshBehaviourProfileData.useCollider")));
                c.Add(ChildProp(nameof(MeshBehaviourProfileData.useLightmap), Loc("MeshBehaviourProfileData.useLightmap")));
                c.Add(ChildProp("staticEditorFlags", Loc("MeshBehaviourProfileData.staticEditorFlags")));
            }).WithEnabled(!MeshWeaverSettings.Current.profiles.Contains(target)));
        }
    }
}