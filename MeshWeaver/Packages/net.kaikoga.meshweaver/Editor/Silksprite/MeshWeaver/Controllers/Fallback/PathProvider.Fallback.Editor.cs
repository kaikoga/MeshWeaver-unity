using Silksprite.MeshWeaver.Controllers.Base;
using UnityEditor;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.Controllers.Fallback
{
    [CustomEditor(typeof(PathProvider), true, isFallback = true)]
    [CanEditMultipleObjects]
    public sealed class PathProviderFallbackEditor : PathProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(PopulateDefaultInspectorGUI());
        }
    }
}