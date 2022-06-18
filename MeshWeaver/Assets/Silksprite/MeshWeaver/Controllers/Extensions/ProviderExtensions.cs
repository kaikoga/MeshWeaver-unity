using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Controllers.Paths;
using Silksprite.MeshWeaver.Models.DataObjects;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Modifiers;
using Silksprite.MeshWeaver.Models.Paths;
using Silksprite.MeshWeaver.Models.Paths.Core;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Extensions
{
    public static class ProviderExtensions
    {
        public static VertexProvider CreateVertex(this CompositePathProvider parent, Vector3 position, bool crease)
        {
            var gameObject = new GameObject("VertexProvider");
            gameObject.transform.SetParent(parent.transform, false);
            gameObject.transform.localPosition = position;
            var vertexProvider = gameObject.AddComponent<VertexProvider>();
            vertexProvider.crease = crease;
            return vertexProvider;
        }
        
        public static VertexProvider CreateVertex(this CompositePathProvider parent, Vector3 position, bool crease, params Vector2[] uvs)
        {
            var vertexProvider = CreateVertex(parent, position, crease);
            vertexProvider.uvs = uvs.Select((uv, i) => new Vector2MuxData { value = uv, channel = i }).ToArray();
            return vertexProvider;
        }
        
        public static CompositePathProvider CreateChildPathProvider(this CompositePathProvider parent, Vector3 position = default)
        {
            var gameObject = new GameObject("CompositePathProvider");
            gameObject.transform.SetParent(parent.transform, false);
            gameObject.transform.localPosition = position;
            return gameObject.AddComponent<CompositePathProvider>();
        }

        public static IPathieFactory CollectPathie(this PathProvider pathProvider)
        {
            if (pathProvider == null) return PathieFactory.Empty;

            var localTranslation = pathProvider.transform.ToLocalMatrix();
            return ModifiedPathieFactory.Builder(pathProvider.ToFactory()).Concat(new VertwiseTranslate(localTranslation)).ToFactory();
        }

        public static IPathieFactory CollectPathies(this IEnumerable<PathProvider> pathProviders, bool isLoop)
        {
            var builder = CompositePathieFactory.Builder(isLoop);
            
            foreach (var pathProvider in pathProviders.Where(c => c != null && c.gameObject.activeSelf))
            {
                var localTranslation = pathProvider.transform.ToLocalMatrix();
                builder.Concat(pathProvider.ToFactory(), localTranslation);
            }

            return builder.ToFactory();
        }

        public static IMeshieFactory CollectMeshie(this MeshProvider meshProvider, IMeshContext context)
        {
            if (meshProvider == null) return MeshieFactory.Empty;

            var localTranslation = meshProvider.transform.ToLocalMatrix();
            return CompositeMeshieFactory.Builder().Concat(meshProvider.ToFactory(context), localTranslation).ToFactory();
        }

        public static IMeshieFactory CollectMeshies(this IEnumerable<MeshProvider> meshProviders, IMeshContext context)
        {
            var builder = CompositeMeshieFactory.Builder();

            foreach (var meshProvider in meshProviders.Where(c => c != null && c.gameObject.activeSelf))
            {
                var localTranslation = meshProvider.transform.ToLocalMatrix();
                builder.Concat(meshProvider.ToFactory(context), localTranslation);
            }

            return builder.ToFactory();
        }
    }
}