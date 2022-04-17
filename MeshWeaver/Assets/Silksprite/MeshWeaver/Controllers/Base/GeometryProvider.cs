using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Paths;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    [DisallowMultipleComponent]
    public class GeometryProvider : MonoBehaviour
    {
        public Matrix4x4 Translation => transform.ToLocalMatrix();

        protected static IPathieFactory CollectPathie(PathProvider pathProvider)
        {
            if (pathProvider == null) return PathieFactory.Empty;

            return CompositePathieFactory.Builder().Concat(pathProvider.ToFactory(), pathProvider.Translation).ToFactory();
        }

        protected static IPathieFactory CollectPathies(IEnumerable<PathProvider> pathProviders)
        {
            var builder = CompositePathieFactory.Builder();
            
            foreach (var pathProvider in pathProviders.Where(c => c != null && c.gameObject.activeSelf))
            {
                builder.Concat(pathProvider.ToFactory(), pathProvider.Translation);
            }

            return builder.ToFactory();
        }

        protected static IMeshieFactory CollectMeshie(MeshProvider meshProvider)
        {
            if (meshProvider == null) return MeshieFactory.Empty;

            return CompositeMeshieFactory.Builder().Concat(meshProvider.ToFactory(), meshProvider.Translation).ToFactory();
        }

        protected static IMeshieFactory CollectMeshies(IEnumerable<MeshProvider> meshProviders)
        {
            var builder = CompositeMeshieFactory.Builder();

            foreach (var meshProvider in meshProviders.Where(c => c != null && c.gameObject.activeSelf))
            {
                builder.Concat(meshProvider.ToFactory(), meshProvider.Translation);
            }

            return builder.ToFactory();
        }
    }
}