using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Controllers
{
    public abstract class MeshProvider : GeometryProvider
    {
        public abstract Meshie ToMeshie();

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