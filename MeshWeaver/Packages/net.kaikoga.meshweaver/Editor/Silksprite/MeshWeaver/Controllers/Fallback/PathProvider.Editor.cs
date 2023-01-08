using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;

namespace Silksprite.MeshWeaver.Controllers.Fallback
{
    [CustomEditor(typeof(PathProvider), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class PathProviderEditor : PathProviderEditorBase
    {
        protected override void OnPropertiesGUI()
        {
            OnBaseInspectorGUI();

            var pathProvider = (PathProvider)target;
            PathModifierProviderMenus.Menu.ModifierPopup(pathProvider);
        }
    }
}