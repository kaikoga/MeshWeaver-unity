using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Meshes;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    public class PolygonMeshProvider : MeshProvider
    {
        public PathProvider pathProvider;

        public override Meshie ToMeshie()
        {
            return new PolygonMeshieFactory().Build(CollectPathie(pathProvider, new Pathie(), false), new Meshie());
        }
    }
}