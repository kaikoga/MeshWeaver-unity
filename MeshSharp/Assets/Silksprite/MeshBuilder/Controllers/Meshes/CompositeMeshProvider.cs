using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    public class CompositeMeshProvider : MeshProvider
    {
        protected override Meshie GenerateMeshie(LodMaskLayer lod)
        {
            return CollectMeshies(this.GetComponentsInDirectChildren<MeshProvider>(), lod);
        }
    }
}