using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    public class PathReference : PathProvider
    {
        protected override bool RefreshAlways => true;

        public List<PathProvider> pathProviders = new List<PathProvider>();
        public bool isLoop;

        protected override IPathieFactory CreateFactory()
        {
            return pathProviders.CollectPathies(isLoop);
        }
    }
}