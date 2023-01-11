using Silksprite.MeshWeaver.Controllers.Base;
using UnityEditor;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.Controllers.Fallback
{
    [CustomEditor(typeof(PathProvider), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class PathProviderEditor : PathProviderEditorBase
    {
        protected override bool IsMainComponentEditor => true;

        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(PopulateDefaultInspectorGUI());
        }
    }
}