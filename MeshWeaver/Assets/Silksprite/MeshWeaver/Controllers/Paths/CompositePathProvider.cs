using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    public class CompositePathProvider : PathProvider
    {
        protected override bool RefreshOnHierarchyChanged => true;

        public bool isLoop;

        protected override IPathieFactory CreateFactory()
        {
            return this.GetComponentsInDirectChildren<PathProvider>().CollectPathies(isLoop);
        }
    }
}