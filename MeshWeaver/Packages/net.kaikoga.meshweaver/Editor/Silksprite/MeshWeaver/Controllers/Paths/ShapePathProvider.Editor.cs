using Silksprite.MeshWeaver.Controllers.Fallback;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    [CustomEditor(typeof(ShapePathProvider))]
    [CanEditMultipleObjects]
    public class ShapePathProviderEditor : PathProviderEditor
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(ShapePathProvider.kind), Loc("ShapePathProvider.kind")));
        }
    }
}