using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Controllers.Extensions;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Meshes;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    public class CompositeMeshProvider : MeshProvider
    {
        protected override IMeshieFactory CreateFactory()
        {
            return CollectMeshies(this.GetComponentsInDirectChildren<MeshProvider>());
        }
    }
}