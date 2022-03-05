using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    public abstract class MeshProvider : GeometryProvider
    {
        public Meshie ToMeshie()
        {
            return GenerateMeshie();
        }

        protected abstract Meshie GenerateMeshie();
    }
}