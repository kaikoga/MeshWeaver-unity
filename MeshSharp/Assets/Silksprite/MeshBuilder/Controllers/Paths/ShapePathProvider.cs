using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Paths;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    public class ShapePathProvider : PathProvider
    {
       public ShapePathieFactory.ShapePathieKind kind;

        protected override Pathie GeneratePathie()
        {
            return new ShapePathieFactory(kind).Build();
        }
    }
}