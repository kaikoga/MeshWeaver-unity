using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Meshes;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    public class SweepMeshProvider : MeshProvider
    {
        public PathProvider pathProviderX;
        public PathProvider pathProviderY;

        public override Meshie ToMeshie()
        {
            return new SweepMeshieFactory().Build(
                CollectPathie(pathProviderX, new Pathie(), false),
                CollectPathie(pathProviderY, new Pathie(), false),
                new Meshie());
        }
    }
}