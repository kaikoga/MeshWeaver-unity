using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.DataObjects;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    public class BakedPathProvider : PathProvider
    {
        public LodMaskLayer[] lodMaskLayers;
        public PathieData[] pathData;

        protected override IPathieFactory CreateFactory()
        {
            if (lodMaskLayers == null || pathData == null) return PathieFactory.Empty;

            var dict = lodMaskLayers
                .Zip(pathData, (lod, pathie) => (lod, pathie))
                .ToDictionary(kv => kv.lod, kv => kv.pathie);
            return new BakedPathieFactory(dict);        }
    }
}