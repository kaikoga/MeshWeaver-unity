using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Meshes;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    public class PolygonMeshProvider : MeshProvider
    {
        public PathProvider pathProvider;

        protected override Meshie GenerateMeshie(LodMask lod)
        {
            return new PolygonMeshieFactory2().Build(CollectPathie(pathProvider, lod));
        }
    }
}