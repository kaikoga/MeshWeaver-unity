using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Core;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    public class PathReference : PathProvider
    {
        public List<PathProvider> pathProviders = new List<PathProvider>();
        readonly PathieCollector _pathProvidersCollector = new PathieCollector();

        public bool isLoop;
        public bool smoothJoin;

        protected override bool Sync() => _pathProvidersCollector.Sync(pathProviders);

        protected override IPathieFactory CreateFactory() => _pathProvidersCollector.CompositeValue(isLoop, smoothJoin);
    }
}