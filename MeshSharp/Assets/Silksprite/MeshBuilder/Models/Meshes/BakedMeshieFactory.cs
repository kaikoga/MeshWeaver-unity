using System.Linq;
using Silksprite.MeshBuilder.Models.DataObjects;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class BakedMeshieFactory
    {
        readonly MeshieData _data;

        public BakedMeshieFactory(MeshieData data)
        {
            _data = data;
        }

        public Meshie Build()
        {
            var meshie = new Meshie();
            if (_data == null) return meshie;
            meshie.Vertices.AddRange(_data.vertices.Select(v => v.ToVertie()));
            meshie.Indices.AddRange(_data.indices);
            return meshie;
        }
    }
}