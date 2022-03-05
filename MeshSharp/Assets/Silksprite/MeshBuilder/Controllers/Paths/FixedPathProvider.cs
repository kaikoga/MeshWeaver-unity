using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Paths;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    public class FixedPathProvider : PathProvider
    {
        protected override Pathie GeneratePathie()
        {
            return new FixedPathieFactory().Build(new Pathie());
        }
    }
}