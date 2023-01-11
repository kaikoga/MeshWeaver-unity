using Silksprite.MeshWeaver.Controllers.Base;
using UnityEditor;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.Controllers.Fallback
{
    [CustomEditor(typeof(ProviderBase), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class SubProviderBaseEditor : ProviderEditorBase
    {
        protected override bool IsMainComponentEditor => false;

        protected sealed override void PopulateInspectorGUI(VisualElement container)
        {
            container.Add(new IMGUIContainer(OnBaseInspectorGUI));
            PopulatePropertiesGUI(container);
        }

        protected virtual void PopulatePropertiesGUI(VisualElement container)
        {
        }
    }
}