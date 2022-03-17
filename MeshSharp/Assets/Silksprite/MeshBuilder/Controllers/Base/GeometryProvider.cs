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

        protected static Pathie CollectPathie(PathProvider pathProvider, LodMask lod)
        {
            var pathie = new Pathie();
            if (pathProvider == null) return pathie;

            pathie.Concat(pathProvider.ToPathie(lod), pathProvider.Translation);

            return pathie;
        }

        protected static Pathie CollectPathies(IEnumerable<PathProvider> pathProviders, LodMask lod)
        {
            var pathie = new Pathie();
            foreach (var pathProvider in pathProviders.Where(c => c != null && c.gameObject.activeSelf))
            {
                pathie.Concat(pathProvider.ToPathie(lod), pathProvider.Translation);
            }

            return pathie;
        }

        protected static void CollectMeshies(IEnumerable<MeshProvider> meshProviders, LodMask lod, Meshie meshie)
        {
            foreach (var meshProvider in meshProviders.Where(c => c != null && c.gameObject.activeSelf))
            {
                meshie.Concat(meshProvider.ToMeshie(lod), meshProvider.Translation, 0);
            }
        }
    }
}