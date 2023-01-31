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

        protected override int SyncReferences()
        {
            return _meshProviderCollector.Sync(meshProvider) ^
                   _pathProviderCollector.Sync(pathProvider);
        }

        protected override IMeshieFactory CreateFactory()
        {
            LastMeshie = _meshProviderCollector.Value;
            LastPathie = _pathProviderCollector.SingleValue();
            return new StampMeshieFactory(LastMeshie, LastPathie);
        }
    }
}