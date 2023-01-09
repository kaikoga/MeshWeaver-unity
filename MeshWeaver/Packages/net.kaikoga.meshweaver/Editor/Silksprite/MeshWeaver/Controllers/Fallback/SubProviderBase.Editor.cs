using Silksprite.MeshWeaver.Controllers.Base;
using UnityEditor;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.Controllers.Fallback
{
    [CustomEditor(typeof(ProviderBase), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class SubProviderBaseEditor : ProviderEditorBase
    {
        public sealed override VisualElement CreateInspectorGUI()
        {
            var container = CreateRootContainerElement();
            container.Add(new IMGUIContainer(() =>
            {
                OnBaseInspectorGUI();
                OnPropertiesGUI();
            }));
            return container;
        }

        protected virtual void OnPropertiesGUI() { }
    }
}