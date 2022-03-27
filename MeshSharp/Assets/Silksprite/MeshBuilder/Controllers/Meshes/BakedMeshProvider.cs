using System;
using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.DataObjects;
using Silksprite.MeshBuilder.Models.Meshes;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    public class BakedMeshProvider : MeshProvider
    {
        public LodMaskLayer[] lodMaskLayers;
        public MeshieData[] meshData;

        public override IMeshieFactory ToFactory(LodMaskLayer lod)
        {
            if (lodMaskLayers == null || meshData == null) return MeshieFactory.Empty;
            var c = Math.Min(lodMaskLayers.Length, meshData.Length);
            for (var i = 0; i < c; i++)
            {
                if (lod == lodMaskLayers[i]) return new BakedMeshieFactory(meshData[i]); 
            }

            return meshData.Length > 0 ? (IMeshieFactory)new BakedMeshieFactory(meshData[0]) : MeshieFactory.Empty;
        }
    }
}