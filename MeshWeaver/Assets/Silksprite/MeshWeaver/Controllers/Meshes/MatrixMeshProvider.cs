using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class MatrixMeshProvider : MeshProvider
    {
        public PathProvider pathProviderX;
        public PathProvider pathProviderY;
        public MatrixMeshieFactory.OperatorKind operatorKind = MatrixMeshieFactory.OperatorKind.ApplyX;

        public int materialIndex;

        public IPathieFactory LastPathieX { get; private set; }
        public IPathieFactory LastPathieY { get; private set; }

        protected override IMeshieFactory CreateFactory()
        {
            LastPathieX = CollectPathie(pathProviderX);
            LastPathieY = CollectPathie(pathProviderY);
            return new MatrixMeshieFactory(LastPathieX, LastPathieY, operatorKind, materialIndex);
        }
    }
}