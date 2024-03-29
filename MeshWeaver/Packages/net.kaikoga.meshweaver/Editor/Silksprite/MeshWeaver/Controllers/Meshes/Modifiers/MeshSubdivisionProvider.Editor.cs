using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    [CustomEditor(typeof(MeshSubdivisionProvider))]
    [CanEditMultipleObjects]
    public class MeshSubdivisionProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(MeshSubdivisionProvider.count), Loc("MeshSubdivisionProvider.count")));
        }
    }
}