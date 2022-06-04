using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.DataObjects;
using Silksprite.MeshWeaver.Models.Meshes;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class BakedMeshProvider : MeshProvider
    {
        public LodMaskLayer[] lodMaskLayers;
        public MeshieData[] meshData;

        public Material[] materials;

        protected override IMeshieFactory CreateFactory(IMeshContext context)
        {
            if (lodMaskLayers == null || meshData == null) return MeshieFactory.Empty;

            var dict = lodMaskLayers
                .Zip(meshData, (lod, meshie) => (lod, meshie))
                .ToDictionary(kv => kv.lod, kv => kv.meshie);
            return new BakedMeshieFactory(dict, i => context.GetMaterialIndex(materials[i]));
        }
    }
}