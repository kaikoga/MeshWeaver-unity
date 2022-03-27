using System.Collections.Generic;
using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Paths;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    public class PathReference : PathProvider
    {
        public List<PathProvider> pathProviders = new List<PathProvider>();

        public override IPathieFactory ToFactory(LodMaskLayer lod)
        {
            return CollectPathies(pathProviders, lod);
        }
    }
}