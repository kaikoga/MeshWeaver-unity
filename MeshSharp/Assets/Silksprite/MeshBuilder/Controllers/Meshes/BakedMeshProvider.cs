using System.Linq;
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

        protected override IMeshieFactory CreateFactory()
        {
            if (lodMaskLayers == null || meshData == null) return MeshieFactory.Empty;

            var dict = lodMaskLayers
                .Zip(meshData, (lod, meshie) => (lod, meshie))
                .ToDictionary(kv => kv.lod, kv => kv.meshie);
            return new BakedMeshieFactory(dict);
        }
    }
}