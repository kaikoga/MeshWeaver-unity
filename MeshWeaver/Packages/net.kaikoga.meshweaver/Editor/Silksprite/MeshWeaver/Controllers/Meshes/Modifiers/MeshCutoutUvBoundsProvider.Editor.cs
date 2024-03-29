using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    [CustomEditor(typeof(MeshCutoutUvBoundsProvider))]
    [CanEditMultipleObjects]
    public class MeshCutoutUvBoundsProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(MeshCutoutUvBoundsProvider.uvAreas), Loc("MeshCutoutUvBoundsProvider.uvAreas")));
            container.Add(Prop(nameof(MeshCutoutUvBoundsProvider.uvChannel), Loc("MeshCutoutUvBoundsProvider.uvChannel")));
            container.Add(Prop(nameof(MeshCutoutUvBoundsProvider.inside), Loc("MeshCutoutUvBoundsProvider.inside")));
            container.Add(Prop(nameof(MeshCutoutUvBoundsProvider.numVertex), Loc("MeshCutoutUvBoundsProvider.numVertex")));
        }
    }
}