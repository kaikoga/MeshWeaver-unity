using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
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
            container.Add(new IMGUIContainer(OnBaseInspectorGUI));
            container.Add(PathModifierProviderMenus.Menu.VisualElement((PathProvider)target));
        }
    }
}