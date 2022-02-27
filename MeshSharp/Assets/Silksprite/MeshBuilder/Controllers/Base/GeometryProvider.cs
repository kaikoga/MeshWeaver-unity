using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    public class GeometryProvider : MonoBehaviour
    {
        public Matrix4x4 Translation => transform.localToWorldMatrix;

        protected Pathie CollectPathies(IEnumerable<PathProvider> pathProviders, Pathie pathie)
        {
            var inverse = Translation.inverse;
            foreach (var pathProvider in pathProviders.Where(c => c != null && c.isActiveAndEnabled))
            {
                pathie.Concat(pathProvider.ToPathie(), inverse * pathProvider.Translation);
            }

            return pathie;
        }
    }
}