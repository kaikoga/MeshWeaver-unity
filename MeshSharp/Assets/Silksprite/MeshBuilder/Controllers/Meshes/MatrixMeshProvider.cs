using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models.Meshes;
using Silksprite.MeshBuilder.Models.Paths;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    public class MatrixMeshProvider : MeshProvider
    {
        public PathProvider pathProviderX;
        public PathProvider pathProviderY;

        public IPathieFactory LastPathieX { get; private set; }
        public IPathieFactory LastPathieY { get; private set; }

        protected override IMeshieFactory CreateFactory()
        {
            LastPathieX = CollectPathie(pathProviderX);
            LastPathieY = CollectPathie(pathProviderY);
            return new MatrixMeshieFactory(LastPathieX, LastPathieY);
        }
    }
}