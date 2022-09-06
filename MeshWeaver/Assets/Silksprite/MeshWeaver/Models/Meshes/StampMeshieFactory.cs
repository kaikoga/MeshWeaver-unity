using System.Linq;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Models.Meshes
{
    public class StampMeshieFactory : IMeshieFactory
    {
        readonly IMeshieFactory _meshie;
        readonly IPathieFactory _pathie;

        public StampMeshieFactory(IMeshieFactory meshie, IPathieFactory pathie)
        {
            _meshie = meshie;
            _pathie = pathie;
        }

        public Meshie Build(LodMaskLayer lod)
        {
            var meshie = _meshie.Build(lod);
            var pathie = _pathie.Build(lod);

            return pathie.Vertices.Aggregate(Meshie.Builder(), (builder, vertie) => builder.Concat(meshie, vertie.Translation, 0)).ToMeshie();
        }

        public Pathie Extract(string pathName, LodMaskLayer lod) => _pathie.Build(lod);
    }
}