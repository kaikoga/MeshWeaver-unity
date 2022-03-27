using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Paths;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    public class FixedPathProvider : PathProvider
    {
        protected override IPathieFactory CreateFactory(LodMaskLayer lod)
        {
            return new FixedPathieFactory();
        }
    }
}