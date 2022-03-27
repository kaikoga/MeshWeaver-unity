using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Paths;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    public class FixedPathProvider : PathProvider
    {
        public override IPathieFactory ToFactory(LodMaskLayer lod)
        {
            return new FixedPathieFactory();
        }
    }
}