using System.Collections.Generic;
using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    public class CompositePathProvider : PathProvider
    {
        public List<PathProvider> pathProviders = new List<PathProvider>();

        protected override Pathie GeneratePathie()
        {
            return CollectPathies(pathProviders, new Pathie(), false);
        }
    }
}