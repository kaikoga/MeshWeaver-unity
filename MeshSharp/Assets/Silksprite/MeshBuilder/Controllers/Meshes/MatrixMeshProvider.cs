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

        protected override IMeshieFactory CreateFactory()
        {
            var pathieX = CollectPathie(pathProviderX);
            var pathieY = CollectPathie(pathProviderY);
            LastPathieX = pathieX.Build(LodMaskLayer.LOD0); // FIXME
            LastPathieY = pathieY.Build(LodMaskLayer.LOD0); // FIXME
            return new MatrixMeshieFactory(pathieX, pathieY);
        }
    }
}