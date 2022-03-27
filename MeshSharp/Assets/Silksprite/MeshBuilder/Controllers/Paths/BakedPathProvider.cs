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

        protected override Pathie GeneratePathie(LodMaskLayer lod)
        {
            if (lodMaskLayers == null || pathData == null) return Pathie.Empty();

            var c = Math.Min(lodMaskLayers.Length, pathData.Length);
            for (var i = 0; i < c; i++)
            {
                if (lod == lodMaskLayers[i]) return new BakedPathieFactory(pathData[i]).Build(lod); 
            }

            return pathData.Length > 0 ? new BakedPathieFactory(pathData[0]).Build(lod) : Pathie.Empty();
        }
    }
}