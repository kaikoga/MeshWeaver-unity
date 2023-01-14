using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    [CustomEditor(typeof(MeshCutoutColliderProvider))]
    [CanEditMultipleObjects]
    public class MeshCutoutColliderProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(MeshCutoutColliderProvider.predicates), Loc("MeshCutoutColliderProvider.predicates")));
            container.Add(ColliderMenus.Menu.ToGUIAction((MeshCutoutColliderProvider)target, "Predicate",
                serializedObject.FindProperty(nameof(MeshCutoutColliderProvider.predicates))));
            container.Add(Prop(nameof(MeshCutoutColliderProvider.inside), Loc("MeshCutoutColliderProvider.inside")));
            container.Add(Prop(nameof(MeshCutoutColliderProvider.numVertex), Loc("MeshCutoutColliderProvider.numVertex")));
        }
    }
}