using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Controllers.Utils;
using UnityEditor;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    [CustomEditor(typeof(CompositePathProvider))]
    public class CompositePathProviderEditor : PathProviderEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var compositePathProvider = (CompositePathProvider)target;
            PathProviderMenus.ElementsMenu.PropertyField(compositePathProvider, ref compositePathProvider.pathProviders);

            PathModifierProviderMenus.Menu.ModifierPopup(compositePathProvider);
        }
    }
}