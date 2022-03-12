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

        protected static Pathie CollectPathie(PathProvider pathProvider, LodMask lod, bool applyTranslation)
        {
            var pathie = new Pathie();
            if (pathProvider == null) return pathie;

            if (applyTranslation)
            {
                pathie.Concat(pathProvider.ToPathie(lod), pathProvider.Translation);
            }
            else
            {
                pathie.Concat(pathProvider.ToPathie(lod), pathProvider.Translation);
            }

            return pathie;
        }

        protected static Pathie CollectPathies(IEnumerable<PathProvider> pathProviders, LodMask lod, bool applyTranslation)
        {
            var pathie = new Pathie();
            foreach (var pathProvider in pathProviders.Where(c => c != null && c.gameObject.activeSelf))
            {
                if (applyTranslation)
                {
                    pathie.Concat(pathProvider.ToPathie(lod), pathProvider.Translation);
                }
                else
                {
                    pathie.Concat(pathProvider.ToPathie(lod), pathProvider.Translation);
                }
            }

            return pathie;
        }
    }
}