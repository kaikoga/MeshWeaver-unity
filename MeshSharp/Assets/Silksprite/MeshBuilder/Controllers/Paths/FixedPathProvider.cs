using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Paths;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    public class FixedPathProvider : PathProvider
    {
        public override Pathie ToPathie()
        {
            return new FixedPathieFactory().Build(new Pathie());
        }
    }
}