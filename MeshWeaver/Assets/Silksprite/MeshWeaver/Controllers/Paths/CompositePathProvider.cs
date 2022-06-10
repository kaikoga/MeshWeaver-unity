using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    public class CompositePathProvider : PathProvider
    {
        public bool isLoop;

        protected override IPathieFactory CreateFactory()
        {
            return CollectPathies(this.GetComponentsInDirectChildren<PathProvider>(), isLoop);
        }
    }
}