using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models.Meshes;
using Silksprite.MeshBuilder.Models.Paths;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    public class PillarMeshProvider : MeshProvider
    {
        public bool fillBody = true;
        public bool fillBottom;
        public bool fillTop;

        public int uvChannelBody;
        public int uvChannelBottom;
        public int uvChannelTop;

        public int materialIndexBody;
        public int materialIndexBottom;
        public int materialIndexTop;

        public PathProvider pathProviderX;
        public PathProvider pathProviderY;

        public MatrixMeshieFactory.OperatorKind operatorKind = MatrixMeshieFactory.OperatorKind.ApplyX;
        public PillarMeshieFactory.LongitudeAxisKind longitudeAxisKind = PillarMeshieFactory.LongitudeAxisKind.Y;

        public IPathieFactory LastPathieX { get; private set; }
        public IPathieFactory LastPathieY { get; private set; }

        protected override IMeshieFactory CreateFactory()
        {
            LastPathieX = CollectPathie(pathProviderX);
            LastPathieY = CollectPathie(pathProviderY);
            return new PillarMeshieFactory(LastPathieX,
                LastPathieY,
                operatorKind,
                longitudeAxisKind,
                fillBody,
                fillBottom,
                fillTop,
                uvChannelBody,
                uvChannelBottom,
                uvChannelTop,
                materialIndexBody,
                materialIndexBottom,
                materialIndexTop);
        }
    }
}