using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    public class CompositePathProvider : PathProvider
    {
        protected override Pathie GeneratePathie(LodMaskLayer lod)
        {
            return CollectPathies(this.GetComponentsInDirectChildren<PathProvider>(), lod);
        }

    }
}