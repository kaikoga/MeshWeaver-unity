using System.Collections.Generic;
using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    public class PathReference : PathProvider
    {
        public List<PathProvider> pathProviders = new List<PathProvider>();

        protected override Pathie GeneratePathie(LodMaskLayer lod)
        {
            return CollectPathies(pathProviders, lod).Build(lod);
        }
    }
}