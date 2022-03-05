using System.Collections.Generic;
using System.Linq;
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
            
            ModifierMenu.ModifierPopup(compositePathProvider);
        }
    }
}