using Silksprite.MeshBuilder.Utils;
using UnityEditor;

namespace Silksprite.MeshBuilder.Controllers
{
    [CustomEditor(typeof(MeshProvider))]
    public class MeshProviderEditor : Editor
    {
        static readonly ComponentPopupMenu<PathProvider> PathProviderMenu = new ComponentPopupMenu<PathProvider>
        (
            typeof(PathProvider)
        );

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var meshProvider = (MeshProvider)target;
            var child = PathProviderMenu.ChildPopup(meshProvider.transform);
            if (child != null)
            {
                meshProvider.pathProviders.Add(child);
            }
        }
    }
}