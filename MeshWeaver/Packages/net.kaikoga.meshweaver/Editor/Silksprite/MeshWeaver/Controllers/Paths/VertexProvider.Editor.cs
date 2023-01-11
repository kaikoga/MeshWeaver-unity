using Silksprite.MeshWeaver.Controllers.Fallback;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    [CustomEditor(typeof(VertexProvider))]
    [CanEditMultipleObjects]
    public class VertexProviderEditor : PathProviderEditor
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(VertexProvider.crease), Loc("VertexProvider.crease")));
            container.Add(Prop(nameof(VertexProvider.uvs), Loc("VertexProvider.uvs")));
        }
    }
}