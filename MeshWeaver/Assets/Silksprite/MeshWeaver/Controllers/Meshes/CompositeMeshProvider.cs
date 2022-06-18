using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Models.Meshes;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class CompositeMeshProvider : MeshProvider
    {
        protected override bool RefreshOnHierarchyChanged => true;

        protected override IMeshieFactory CreateFactory(IMeshContext context)
        {
            return this.GetComponentsInDirectChildren<MeshProvider>().CollectMeshies(context);
        }
    }
}