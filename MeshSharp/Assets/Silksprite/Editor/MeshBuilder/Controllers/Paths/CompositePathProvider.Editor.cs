using Silksprite.MeshBuilder.Utils;
using UnityEditor;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    [CustomEditor(typeof(CompositePathProvider))]
    public class CompositePathProviderEditor : Editor
    {
        static readonly ComponentPopupMenu<PathProvider> PathProviderMenu = new ComponentPopupMenu<PathProvider>
        (
            typeof(PathReference),
            typeof(VertexProvider)
        );

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var compositePathProvider = (CompositePathProvider)target;
            PathProviderMenu.PropertyField(compositePathProvider, ref compositePathProvider.pathProviders);
        }
    }
}