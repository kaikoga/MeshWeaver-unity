using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Meshes;
using Silksprite.MeshBuilder.Models.Paths;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    [DisallowMultipleComponent]
    public class GeometryProvider : MonoBehaviour
    {
        public Matrix4x4 Translation => Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);

        protected static IPathieFactory CollectPathie(PathProvider pathProvider, LodMaskLayer lod)
        {
            if (pathProvider == null) return PathieFactory.Empty;

            return CompositePathieFactory.Builder().Concat(pathProvider.ToFactory(lod), pathProvider.Translation).ToFactory();
        }

        protected static IPathieFactory CollectPathies(IEnumerable<PathProvider> pathProviders, LodMaskLayer lod)
        {
            var builder = CompositePathieFactory.Builder();
            
            foreach (var pathProvider in pathProviders.Where(c => c != null && c.gameObject.activeSelf))
            {
                builder.Concat(pathProvider.ToFactory(lod), pathProvider.Translation);
            }

            return builder.ToFactory();
        }

        protected static IMeshieFactory CollectMeshies(IEnumerable<MeshProvider> meshProviders, LodMaskLayer lod)
        {
            var builder = CompositeMeshieFactory.Builder();

            foreach (var meshProvider in meshProviders.Where(c => c != null && c.gameObject.activeSelf))
            {
                builder.Concat(meshProvider.ToFactory(lod), meshProvider.Translation);
            }

            return builder.ToFactory();
        }
    }
}