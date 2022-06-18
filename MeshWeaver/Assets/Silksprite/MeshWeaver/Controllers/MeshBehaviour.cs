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
            var context = new StaticMeshContext(materials);
            return this.GetComponentsInDirectChildren<MeshProvider>().CollectMeshies(context).Build(lod);
        }

        protected override void OnCollectMaterials()
        {
            var context = new DynamicMeshContext();
            this.GetComponentsInDirectChildren<MeshProvider>().CollectMeshies(context);
            materials = context.ToMaterials();
        }
    }
}