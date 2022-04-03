using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models.DataObjects;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class BakedMeshieFactory : IMeshieFactory
    {
        readonly Dictionary<LodMaskLayer, MeshieData> _data;
        readonly MeshieData _defaultData;

        public BakedMeshieFactory(Dictionary<LodMaskLayer, MeshieData> data)
        {
            _data = data;
        }

        public BakedMeshieFactory(MeshieData data)
        {
            _data = new Dictionary<LodMaskLayer, MeshieData>();
            _defaultData = data;
        }

        public Meshie Build(LodMaskLayer lod)
        {
            if (!_data.TryGetValue(lod, out var data))
            {
                data = _defaultData;
                if (data == null) return Meshie.Empty();
            }

            return data.ToMeshie();
        }
    }
}