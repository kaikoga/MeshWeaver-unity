using System.Linq;
using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.DataObjects;
using Silksprite.MeshBuilder.Models.Paths;

namespace Silksprite.MeshBuilder.Controllers.Paths
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