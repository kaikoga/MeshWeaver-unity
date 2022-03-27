using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Meshes;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    public class CompositeMeshProvider : MeshProvider
    {
        public override IMeshieFactory ToFactory(LodMaskLayer lod)
        {
            return CollectMeshies(this.GetComponentsInDirectChildren<MeshProvider>(), lod);
        }
    }
}