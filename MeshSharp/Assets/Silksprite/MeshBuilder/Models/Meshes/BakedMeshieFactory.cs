using System.Linq;
using Silksprite.MeshBuilder.Models.DataObjects;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class BakedMeshieFactory : IMeshieFactory
    {
        readonly MeshieData _data;

        public BakedMeshieFactory(MeshieData data)
        {
            _data = data;
        }

        public Meshie Build(LodMaskLayer lod)
        {
            if (_data == null) return Meshie.Empty();
            return new Meshie(
                _data.vertices.Select(v => v.ToVertie()),
                _data.indices);
        }
    }
}