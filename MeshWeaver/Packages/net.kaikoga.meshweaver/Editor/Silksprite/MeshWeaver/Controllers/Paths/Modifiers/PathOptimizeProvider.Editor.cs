using Silksprite.MeshWeaver.Controllers.Base;
using UnityEditor;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
{
    [CustomEditor(typeof(PathOptimizeProvider))]
    [CanEditMultipleObjects]
    public class PathOptimizeProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
        }
    }
}