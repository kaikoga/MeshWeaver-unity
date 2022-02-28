using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Meshes;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    public class MatrixMeshProvider : MeshProvider
    {
        public PathProvider pathProviderX;
        public PathProvider pathProviderY;

        public override Meshie ToMeshie()
        {
            return new MatrixMeshieFactory().Build(
                CollectPathie(pathProviderX, new Pathie(), false),
                CollectPathie(pathProviderY, new Pathie(), false),
                new Meshie());
        }
    }
}