using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Meshes;

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

        public PathProvider pathProviderX;
        public PathProvider pathProviderY;

        public Pathie LastPathieX { get; private set; }
        public Pathie LastPathieY { get; private set; }

        protected override IMeshieFactory ToFactory(LodMaskLayer lod)
        {
            var pathieX = CollectPathie(pathProviderX, lod);
            var pathieY = CollectPathie(pathProviderY, lod);
            LastPathieX = pathieX.Build(lod); // FIXME
            LastPathieY = pathieY.Build(lod); // FIXME
            return new PillarMeshieFactory(pathieX,
                pathieY,
                fillBody,
                fillBottom,
                fillTop,
                uvChannelBody,
                uvChannelBottom,
                uvChannelTop);
        }
    }
}