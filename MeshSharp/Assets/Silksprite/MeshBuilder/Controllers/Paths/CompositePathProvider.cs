using System.Collections.Generic;
using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    public class CompositePathProvider : PathProvider, ICompositeGeometry<PathProvider>
    {
        public List<PathProvider> pathProviders = new List<PathProvider>();

        public List<PathProvider> PrimaryElements { set => pathProviders = value; }

        protected override Pathie GeneratePathie(LodMask lod)
        {
            return CollectPathies(pathProviders, lod);
        }

    }
}