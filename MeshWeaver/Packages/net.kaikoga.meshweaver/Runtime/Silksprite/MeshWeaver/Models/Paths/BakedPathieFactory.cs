using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Models.DataObjects;

namespace Silksprite.MeshWeaver.Models.Paths
{
    public class BakedPathieFactory : IPathieFactory
    {
        readonly Dictionary<LodMaskLayer, PathieData> _data;
        readonly PathieData _defaultData;

        public BakedPathieFactory(Dictionary<LodMaskLayer, PathieData> data)
        {
            _data = data;
        }

        public BakedPathieFactory(PathieData data)
        {
            _data = new Dictionary<LodMaskLayer, PathieData>();
            _defaultData = data;
        }

        public Pathie Build(LodMaskLayer lod)
        {
            if (!_data.TryGetValue(lod, out var data))
            {
                data = _defaultData;
                if (data == null) return Pathie.Empty();
            }

            return data.ToPathie();
        }
    }

}