using Silksprite.MeshWeaver.Models.Meshes;

namespace Silksprite.MeshWeaver.Models.Paths
{
    public class ExtractPathieFactory : IPathieFactory
    {
        readonly IMeshieFactory _meshieFactory;
        readonly string _pathName;

        public ExtractPathieFactory(IMeshieFactory meshieFactory, string pathName)
        {
            _meshieFactory = meshieFactory;
            _pathName = pathName;
        }

        public Pathie Build(LodMaskLayer lod) => _meshieFactory.Extract(_pathName, lod);
    }

}