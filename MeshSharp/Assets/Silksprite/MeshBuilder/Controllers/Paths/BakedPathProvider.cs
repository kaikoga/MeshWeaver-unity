using System;
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

        protected override IPathieFactory ToFactory(LodMaskLayer lod)
        {
            if (lodMaskLayers == null || pathData == null) return Pathie.Empty();

            var c = Math.Min(lodMaskLayers.Length, pathData.Length);
            for (var i = 0; i < c; i++)
            {
                if (lod == lodMaskLayers[i]) return new BakedPathieFactory(pathData[i]); 
            }

            return pathData.Length > 0 ? (IPathieFactory)new BakedPathieFactory(pathData[0]) : Pathie.Empty();
        }
    }
}