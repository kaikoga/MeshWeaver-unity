using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Models;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers
{
    [ExecuteAlways]
    public class MeshBehaviour : CustomMeshBehaviour
    {
        protected override Meshie OnPopulateMesh(LodMaskLayer lod)
        {
            using (var context = NullMeshContext.Instance)
            {
                return this.GetComponentsInDirectChildren<MeshProvider>().CollectMeshies(context).Build(lod);
            }
        }
    }
}