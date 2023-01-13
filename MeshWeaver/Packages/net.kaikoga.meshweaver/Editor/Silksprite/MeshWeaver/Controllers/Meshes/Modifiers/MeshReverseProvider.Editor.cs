using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    [CustomEditor(typeof(MeshReverseProvider))]
    [CanEditMultipleObjects]
    public class MeshReverseProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(MeshReverseProvider.front), Loc("MeshReverseProvider.front")));
            container.Add(Prop(nameof(MeshReverseProvider.back), Loc("MeshReverseProvider.back")));
        }
    }
}