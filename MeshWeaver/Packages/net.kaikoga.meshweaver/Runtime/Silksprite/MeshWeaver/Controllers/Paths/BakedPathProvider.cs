using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Models.DataObjects;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    public class BakedPathProvider : PathProvider
    {
        public BakedPathieData[] bakedData;

        protected override IPathieFactory CreateFactory(IMeshContext context)
        {
            if (bakedData == null) return PathieFactory.Empty;

            var dict = bakedData
                .SelectMany(data => data.lodMaskLayers.Select(lod => (lod, data.pathData)))
                .GroupBy(kv => kv.lod)
                .ToDictionary(kv => kv.Key, kv => kv.First().pathData);
            return new BakedPathieFactory(dict);
        }
    }
}