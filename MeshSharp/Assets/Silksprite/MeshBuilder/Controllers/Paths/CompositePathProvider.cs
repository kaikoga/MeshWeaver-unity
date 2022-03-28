using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Controllers.Extensions;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Paths;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    public class CompositePathProvider : PathProvider
    {
        protected override IPathieFactory CreateFactory()
        {
            return CollectPathies(this.GetComponentsInDirectChildren<PathProvider>());
        }

    }
}