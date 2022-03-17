using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.DataObjects;
using Silksprite.MeshBuilder.Models.Meshes;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    public class BakedMeshProvider : MeshProvider
    {
        public MeshieData meshData;

        protected override Meshie GenerateMeshie(LodMaskLayer lod)
        {
            return new BakedMeshieFactory(meshData).Build();
        }
    }
}