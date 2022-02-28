using System.Collections.Generic;
using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    public class PathReference : PathProvider
    {
        public List<PathProvider> pathProviders = new List<PathProvider>();

        public override Pathie ToPathie()
        {
            return CollectPathies(pathProviders, new Pathie(), true);
        }
    }
}