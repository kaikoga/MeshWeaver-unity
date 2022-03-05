using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Controllers.Paths.Modifiers;
using Silksprite.MeshBuilder.Utils;
using UnityEditor;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    [CustomEditor(typeof(CompositePathProvider))]
    public class CompositePathProviderEditor : Editor
    {
        static readonly ChildComponentPopupMenu<PathProvider> PathProviderMenu = new ChildComponentPopupMenu<PathProvider>
        (
            typeof(PathReference),
            typeof(VertexProvider)
        );

        static readonly ModifierComponentPopupMenu<PathModifierProvider> ModifierMenu = new ModifierComponentPopupMenu<PathModifierProvider>
        (
            typeof(UvRemapperProvider)
        );

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var compositePathProvider = (CompositePathProvider)target;
            PathProviderMenu.PropertyField(compositePathProvider, ref compositePathProvider.pathProviders);
            
            ModifierMenu.ModifierPopup(compositePathProvider);
        }
    }
}