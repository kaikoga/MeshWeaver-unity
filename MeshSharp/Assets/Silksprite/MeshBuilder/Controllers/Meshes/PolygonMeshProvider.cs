using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Meshes;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    public class PolygonMeshProvider : MeshProvider
    {
        public PathProvider pathProvider;

        public int materialIndex;

        protected override IMeshieFactory CreateFactory()
        {
            return new PolygonMeshieFactory2(CollectPathie(pathProvider), materialIndex);
        }
    }
}