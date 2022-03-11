using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Meshes;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    public class MatrixMeshProvider : MeshProvider
    {
        public PathProvider pathProviderX;
        public PathProvider pathProviderY;

        public Pathie LastPathieX { get; private set; }
        public Pathie LastPathieY { get; private set; }

        protected override Meshie GenerateMeshie()
        {
            LastPathieX = CollectPathie(pathProviderX, true);
            LastPathieY = CollectPathie(pathProviderY, true);
            return new MatrixMeshieFactory().Build(LastPathieX, LastPathieY);
        }
    }
}