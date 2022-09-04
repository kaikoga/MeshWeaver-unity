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
        static Matrix4x4 ToLocalTranslation(this Component geometryProvider)
        {
            var localMatrix = geometryProvider.transform.ToLocalMatrix();
            if (!geometryProvider.TryGetComponent<TranslationProvider>(out var translation)) return localMatrix;
            var oneX = translation.oneX;
            var oneY = translation.oneY;
            var oneZ = translation.oneZ;
            return localMatrix * new Matrix4x4
            {
                m00 = oneX.x,
                m10 = oneX.y,
                m20 = oneX.z,
                m30 = 0,
                m01 = oneY.x,
                m11 = oneY.y,
                m21 = oneY.z,
                m31 = 0,
                m02 = oneZ.x,
                m12 = oneZ.y,
                m22 = oneZ.z,
                m32 = 0,
                m03 = 0,
                m13 = 0,
                m23 = 0,
                m33 = 1
            };
        }

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

            var localTranslation = pathProvider.ToLocalTranslation();
            // FIXME: maybe we want to cleanup Pathie.isLoop and fix this 
            return ModifiedPathieFactory.Builder(pathProvider.ToFactory(), pathProvider.lodMask).Concat(new VertwiseTranslate(localTranslation)).ToFactory();
        }

        public static IPathieFactory CollectPathies(this IEnumerable<PathProvider> pathProviders, bool isLoop)
        {
            var builder = CompositePathieFactory.Builder(isLoop);
            
            foreach (var pathProvider in pathProviders.Where(c => c != null && c.gameObject.activeSelf))
            {
                var localTranslation = pathProvider.ToLocalTranslation();
                builder.Concat(pathProvider.ToFactory(), localTranslation);
            }

            return builder.ToFactory();
        }

        public static IMeshieFactory CollectMeshie(this MeshProvider meshProvider, IMeshContext context)
        {
            if (meshProvider == null) return MeshieFactory.Empty;

            var localTranslation = meshProvider.ToLocalTranslation();
            return CompositeMeshieFactory.Builder().Concat(meshProvider.ToFactory(context), localTranslation).ToFactory();
        }

        public static IMeshieFactory CollectMeshies(this IEnumerable<MeshProvider> meshProviders, IMeshContext context)
        {
            var builder = CompositeMeshieFactory.Builder();

            foreach (var meshProvider in meshProviders.Where(c => c != null && c.gameObject.activeSelf))
            {
                var localTranslation = meshProvider.ToLocalTranslation();
                builder.Concat(meshProvider.ToFactory(context), localTranslation);
            }

            return builder.ToFactory();
        }
    }
}