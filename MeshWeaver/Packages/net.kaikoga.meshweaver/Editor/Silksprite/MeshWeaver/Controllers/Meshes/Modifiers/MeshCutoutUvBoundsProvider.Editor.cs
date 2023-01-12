using Silksprite.MeshWeaver.Controllers.Base;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    [CustomEditor(typeof(MeshCutoutUvBoundsProvider))]
    [CanEditMultipleObjects]
    public class MeshCutoutUvBoundsProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(MeshCutoutUvBoundsProvider.uvAreas), Loc("MeshCutoutUvBoundsProvider.uvAreas")));
            container.Add(Prop(nameof(MeshCutoutUvBoundsProvider.uvChannel), Loc("MeshCutoutUvBoundsProvider.uvChannel")));
            container.Add(Prop(nameof(MeshCutoutUvBoundsProvider.inside), Loc("MeshCutoutUvBoundsProvider.inside")));
            container.Add(Prop(nameof(MeshCutoutUvBoundsProvider.numVertex), Loc("MeshCutoutUvBoundsProvider.numVertex")));
        }
    }
}