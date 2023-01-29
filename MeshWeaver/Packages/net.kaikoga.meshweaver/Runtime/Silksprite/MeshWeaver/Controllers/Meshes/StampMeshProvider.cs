using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Core;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class StampMeshProvider : MeshProvider
    {
        public MeshProvider meshProvider;
        readonly MeshieCollector _meshProviderCollector = new MeshieCollector();

        public PathProvider pathProvider;
        readonly PathieCollector _pathProviderCollector = new PathieCollector();

        public IMeshieFactory LastMeshie { get; private set; }
        public IPathieFactory LastPathie { get; private set; }

        protected override IMeshieFactory CreateFactory()
        {
            LastMeshie = _meshProviderCollector.CollectMeshie(meshProvider);
            LastPathie = _pathProviderCollector.CollectPathie(pathProvider);
            return new StampMeshieFactory(LastMeshie, LastPathie);
        }
    }
}