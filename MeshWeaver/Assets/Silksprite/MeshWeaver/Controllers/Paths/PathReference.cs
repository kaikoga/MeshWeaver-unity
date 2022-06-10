using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    public class PathReference : PathProvider
    {
        public List<PathProvider> pathProviders = new List<PathProvider>();
        public bool isLoop;

        protected override IPathieFactory CreateFactory()
        {
            return CollectPathies(pathProviders, isLoop);
        }
    }
}