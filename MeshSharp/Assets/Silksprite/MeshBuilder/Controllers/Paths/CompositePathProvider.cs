using System.Collections.Generic;
using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    public class CompositePathProvider : PathProvider
    {
        public List<PathProvider> pathProviders = new List<PathProvider>();
        public bool applyTranslation;

        public override Pathie ToPathie()
        {
            return CollectPathies(pathProviders, new Pathie(), applyTranslation);
        }
    }
}