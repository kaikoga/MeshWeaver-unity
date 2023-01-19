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
        StaticMeshContext _staticContext = new StaticMeshContext(new Material[]{});

        protected override Meshie OnPopulateMesh(LodMaskLayer lod)
        {
            if (overrideMaterials)
            {
                if (materials == null || !_staticContext.SequenceEqual(materials))
                {
                    _staticContext?.Dispose();
                    _staticContext = new StaticMeshContext(materials);
                }

                return this.GetComponentsInDirectChildren<MeshProvider>().CollectMeshies(_staticContext).Build(lod);
            }

            using (var context = new DynamicMeshContext())
            {
                var result = this.GetComponentsInDirectChildren<MeshProvider>().CollectMeshies(context).Build(lod);
                materials = context.ToMaterials();
                return result;
            }
        }
    }
}