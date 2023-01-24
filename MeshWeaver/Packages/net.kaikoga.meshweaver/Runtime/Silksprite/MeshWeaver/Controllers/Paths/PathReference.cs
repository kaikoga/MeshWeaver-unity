using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    public class PathReference : PathProvider
    {
        public List<PathProvider> pathProviders = new List<PathProvider>();
        public bool isLoop;
        public bool smoothJoin;

        protected override IPathieFactory CreateFactory(IMeshContext context)
        {
            return pathProviders.CollectPathies(context, isLoop, smoothJoin);
        }
    }
}