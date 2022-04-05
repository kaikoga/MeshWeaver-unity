using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Models.Meshes;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class CompositeMeshProvider : MeshProvider
    {
        protected override IMeshieFactory CreateFactory()
        {
            return CollectMeshies(this.GetComponentsInDirectChildren<MeshProvider>());
        }
    }
}