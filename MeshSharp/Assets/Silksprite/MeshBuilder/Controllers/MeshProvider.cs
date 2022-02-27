using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Controllers
{
    public abstract class MeshProvider : GeometryProvider
    {
        public abstract Meshie ToMeshie();
    }
}