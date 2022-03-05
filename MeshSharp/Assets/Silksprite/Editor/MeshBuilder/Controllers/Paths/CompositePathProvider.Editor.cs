using System.Collections.Generic;
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

        IEnumerable<VertexProvider> ActiveVertices()
        {
            return ((CompositePathProvider)target).GetComponentsInChildren<VertexProvider>().Where(c => c != null).ToArray();
        }

        public override void OnInspectorGUI()
        {
            void DistributeX(float min, float max)
            {
                var vertexProviders = ActiveVertices().ToList();
                var iMax = vertexProviders.Count - 1;
                foreach (var (v, i) in vertexProviders.Select((v, i) => (v, i)))
                {
                    v.uv.x = min + (max - min) * i / iMax;
                }
            }

            void DistributeY(float min, float max)
            {
                var vertexProviders = ActiveVertices().ToList();
                var iMax = vertexProviders.Count - 1;
                foreach (var (v, i) in vertexProviders.Select((v, i) => (v, i)))
                {
                    v.uv.y = min + (max - min) * i / iMax;
                }
            }

            base.OnInspectorGUI();
            var compositePathProvider = (CompositePathProvider)target;
            PathProviderMenu.PropertyField(compositePathProvider, ref compositePathProvider.pathProviders);
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label("Set UV.u");
                if (GUILayout.Button("0"))
                {
                    DistributeX(0f, 0f);
                }
                if (GUILayout.Button("0..1"))
                {
                    DistributeX(0f, 1f);
                }
                if (GUILayout.Button("1"))
                {
                    DistributeX(1f, 1f);
                }
                if (GUILayout.Button("1..0"))
                {
                    DistributeX(1f, 0f);
                }
            }
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label("Set UV.v");
                if (GUILayout.Button("0"))
                {
                    DistributeY(0f, 0f);
                }
                if (GUILayout.Button("0..1"))
                {
                    DistributeY(0f, 1f);
                }
                if (GUILayout.Button("1"))
                {
                    DistributeY(1f, 1f);
                }
                if (GUILayout.Button("1..0"))
                {
                    DistributeY(1f, 0f);
                }
            }
        }
    }
}