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

        protected override Meshie GenerateMeshie(LodMaskLayer lod)
        {
            if (lodMaskLayers == null || meshData == null) return Meshie.Empty();
            var c = Math.Min(lodMaskLayers.Length, meshData.Length);
            for (var i = 0; i < c; i++)
            {
                if (lod == lodMaskLayers[i]) return new BakedMeshieFactory(meshData[i]).Build(); 
            }

            return meshData.Length > 0 ? new BakedMeshieFactory(meshData[0]).Build() : Meshie.Empty();
        }
    }
}