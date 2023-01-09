using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;

namespace Silksprite.MeshWeaver.Controllers.Fallback
{
    [CustomEditor(typeof(MeshProvider), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class MeshProviderEditor : MeshProviderEditorBase
    {
        protected override bool IsMainComponentEditor => true;

        protected override void OnPropertiesGUI()
        {
            OnBaseInspectorGUI();

            var meshProvider = (MeshProvider)target;
            MeshModifierProviderMenus.Menu.ModifierPopup(meshProvider);
        }
    }
}