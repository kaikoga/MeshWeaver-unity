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

        public override IMeshieFactory ToFactory(LodMaskLayer lod)
        {
            var pathieX = CollectPathie(pathProviderX, lod);
            var pathieY = CollectPathie(pathProviderY, lod);
            LastPathieX = pathieX.Build(lod); // FIXME
            LastPathieY = pathieY.Build(lod); // FIXME
            return new MatrixMeshieFactory(LastPathieX, LastPathieY);
        }
    }
}