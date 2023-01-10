using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.Controllers.Fallback
{
    [CustomEditor(typeof(MeshProvider), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class MeshProviderEditor : MeshProviderEditorBase
    {
        protected override bool IsMainComponentEditor => true;

        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(new IMGUIContainer(() =>
            {
                OnBaseInspectorGUI();

                var meshProvider = (MeshProvider)target;
                MeshModifierProviderMenus.Menu.ModifierPopup(meshProvider);
            }));
        }
    }
}