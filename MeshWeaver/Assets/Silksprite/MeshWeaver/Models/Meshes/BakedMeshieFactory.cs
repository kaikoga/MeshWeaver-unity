using System;
using System.Collections.Generic;
using Silksprite.MeshWeaver.Models.DataObjects;

namespace Silksprite.MeshWeaver.Models.Meshes
{
    public class BakedMeshieFactory : IMeshieFactory
    {
        readonly Dictionary<LodMaskLayer, MeshieData> _data;
        readonly MeshieData _defaultData;
        readonly Func<int, int> _materialMapping;

        public BakedMeshieFactory(Dictionary<LodMaskLayer, MeshieData> data, Func<int, int> materialMapping)
        {
            _data = data;
            _materialMapping = materialMapping ?? (i => i);
        }

        public BakedMeshieFactory(MeshieData data, Func<int, int> materialMapping)
        {
            _data = new Dictionary<LodMaskLayer, MeshieData>();
            _defaultData = data;
            _materialMapping = materialMapping ?? (i => i);
        }

        public Meshie Build(LodMaskLayer lod)
        {
            if (!_data.TryGetValue(lod, out var data))
            {
                data = _defaultData;
                if (data == null) return Meshie.Empty();
            }

            return data.ToMeshie(_materialMapping);
        }
    }
}