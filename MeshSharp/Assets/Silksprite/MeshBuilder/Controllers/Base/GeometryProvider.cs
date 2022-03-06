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

        protected Pathie CollectPathie(PathProvider pathProvider, Pathie pathie, bool applyTranslation)
        {
            if (pathProvider == null) return pathie;

            if (applyTranslation)
            {
                pathie.Concat(pathProvider.ToPathie(), pathProvider.Translation);
            }
            else
            {
                pathie.Concat(pathProvider.ToPathie(), pathProvider.Translation);
            }

            return pathie;
        }

        protected Pathie CollectPathies(IEnumerable<PathProvider> pathProviders, Pathie pathie, bool applyTranslation)
        {
            foreach (var pathProvider in pathProviders.Where(c => c != null && c.isActiveAndEnabled))
            {
                if (applyTranslation)
                {
                    pathie.Concat(pathProvider.ToPathie(), pathProvider.Translation);
                }
                else
                {
                    pathie.Concat(pathProvider.ToPathie(), pathProvider.Translation);
                }
            }

            return pathie;
        }
    }
}