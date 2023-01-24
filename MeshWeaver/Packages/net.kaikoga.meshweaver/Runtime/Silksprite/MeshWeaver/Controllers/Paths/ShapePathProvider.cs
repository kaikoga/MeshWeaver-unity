using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    public class ShapePathProvider : PathProvider
    {
       public ShapePathieFactory.ShapePathieKind kind;

       protected override IPathieFactory CreateFactory(IMeshContext context)
        {
            return new ShapePathieFactory(kind);
        }
    }
}