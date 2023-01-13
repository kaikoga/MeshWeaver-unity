using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    [CustomEditor(typeof(VertexProvider))]
    [CanEditMultipleObjects]
    public class VertexProviderEditor : PathProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(VertexProvider.crease), Loc("VertexProvider.crease")));
            container.Add(Prop(nameof(VertexProvider.uvs), Loc("VertexProvider.uvs")));
        }
    }
}