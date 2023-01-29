using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class StampMeshProvider : MeshProvider
    {
        public MeshProvider meshProvider;
        public PathProvider pathProvider;

        public IMeshieFactory LastMeshie { get; private set; }
        public IPathieFactory LastPathie { get; private set; }

        protected override IMeshieFactory CreateFactory()
        {
            LastMeshie = meshProvider.CollectMeshie();
            LastPathie = pathProvider.CollectPathie();
            return new StampMeshieFactory(LastMeshie, LastPathie);
        }
    }
}