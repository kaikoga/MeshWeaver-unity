using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    public class CompositePathProvider : PathProvider
    {
        public bool isLoop;
        public bool smoothJoin;

        protected override IPathieFactory CreateFactory(IMeshContext context)
        {
            return this.GetComponentsInDirectChildren<PathProvider>().CollectPathies(context, isLoop, smoothJoin);
        }
    }
}