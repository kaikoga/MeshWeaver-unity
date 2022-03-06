using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    public abstract class MeshProvider : GeometryProvider
    {
        public Meshie LastMeshie { get; private set; }

        public Meshie ToMeshie()
        {
            LastMeshie = GenerateMeshie();
            return LastMeshie;
        }

        protected abstract Meshie GenerateMeshie();
    }
}