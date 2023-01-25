using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Models.DataObjects;
using Silksprite.MeshWeaver.Models.Meshes;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class BakedMeshProvider : MeshProvider
    {
        public BakedMeshieData[] bakedData;

        public Material[] materials;

        protected override IMeshieFactory CreateFactory(IMeshContext context)
        {
            if (bakedData == null) return MeshieFactory.Empty;

            foreach (var material in materials)
            {
            }

            var dict = bakedData
                .SelectMany(data => data.lodMaskLayers.Select(lod => (lod, data.meshData)))
                .GroupBy(kv => kv.lod)
                .ToDictionary(kv => kv.Key, kv => kv.First().meshData);
            return new BakedMeshieFactory(dict, i => materials[i]);
        }
    }
}