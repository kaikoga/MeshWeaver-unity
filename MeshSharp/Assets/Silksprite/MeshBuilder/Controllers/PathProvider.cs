using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Controllers
{
    public abstract class PathProvider : GeometryProvider
    {
        public abstract Pathie ToPathie();
    }
}