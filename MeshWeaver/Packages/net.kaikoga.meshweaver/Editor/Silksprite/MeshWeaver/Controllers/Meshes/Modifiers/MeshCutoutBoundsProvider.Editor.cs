using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    [CustomEditor(typeof(MeshCutoutBoundsProvider))]
    [CanEditMultipleObjects]
    public class MeshCutoutBoundsProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(MeshCutoutBoundsProvider.boundsData), Loc("MeshCutoutBoundsProvider.boundsData")));
            container.Add(Prop(nameof(MeshCutoutBoundsProvider.inside), Loc("MeshCutoutBoundsProvider.inside")));
            container.Add(Prop(nameof(MeshCutoutBoundsProvider.numVertex), Loc("MeshCutoutBoundsProvider.numVertex")));
        }
    }
}