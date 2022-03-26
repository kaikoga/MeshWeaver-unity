using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    [DisallowMultipleComponent]
    public class GeometryProvider : MonoBehaviour
    {
        public Matrix4x4 Translation => Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);

        protected static Pathie CollectPathie(PathProvider pathProvider, LodMaskLayer lod)
        {
            if (pathProvider == null) return Pathie.Empty();

            return Pathie.Builder().Concat(pathProvider.ToPathie(lod), pathProvider.Translation).ToPathie();
        }

        protected static Pathie CollectPathies(IEnumerable<PathProvider> pathProviders, LodMaskLayer lod)
        {
            var builder = Pathie.Builder();
            
            foreach (var pathProvider in pathProviders.Where(c => c != null && c.gameObject.activeSelf))
            {
                builder.Concat(pathProvider.ToPathie(lod), pathProvider.Translation);
            }

            return builder.ToPathie();
        }

        protected static Meshie CollectMeshies(IEnumerable<MeshProvider> meshProviders, LodMaskLayer lod)
        {
            var builder = Meshie.Builder();

            foreach (var meshProvider in meshProviders.Where(c => c != null && c.gameObject.activeSelf))
            {
                builder.Concat(meshProvider.ToMeshie(lod), meshProvider.Translation, 0);
            }

            return builder.ToMeshie();
        }
    }
}