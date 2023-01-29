using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Core;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    public class CompositePathProvider : PathProvider
    {
        readonly PathieCollector _childrenCollector = new PathieCollector();

        public bool isLoop;
        public bool smoothJoin;

        protected override void Sync() => _childrenCollector.Sync(this.GetComponentsInDirectChildren<PathProvider>());

        protected override IPathieFactory CreateFactory() => _childrenCollector.CompositeValue(isLoop, smoothJoin);
    }
}