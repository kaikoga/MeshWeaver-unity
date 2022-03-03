using System.Linq;
using Silksprite.MeshBuilder.Utils;
using UnityEditor;
using UnityEngine;

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
            var child = PathProviderMenu.ChildPopup(compositePathProvider.transform);
            if (child != null)
            {
                compositePathProvider.pathProviders.Add(child);
            }

            if (GUILayout.Button("Collect"))
            {
                compositePathProvider.pathProviders = compositePathProvider.transform.OfType<Transform>()
                    .Select(t => t.GetComponent<PathProvider>())
                    .Where(p => p != null)
                    .ToList();
                EditorUtility.SetDirty(compositePathProvider);
            }
        }
    }
}