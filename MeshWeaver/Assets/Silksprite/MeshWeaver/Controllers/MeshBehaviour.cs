using System.Linq;
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
        Material[] _lastMaterials = { };

        protected override Meshie OnPopulateMesh(LodMaskLayer lod)
        {
            if (!_lastMaterials.SequenceEqual(materials))
            {
                _staticContext?.Dispose();
                _lastMaterials = materials.ToArray();
                _staticContext = new StaticMeshContext(_lastMaterials);
            }

            return this.GetComponentsInDirectChildren<MeshProvider>().CollectMeshies(_staticContext).Build(lod);
        }

        protected override void OnCollectMaterials()
        {
            using (var context = new DynamicMeshContext())
            {
                this.GetComponentsInDirectChildren<MeshProvider>().CollectMeshies(context);
                materials = context.ToMaterials();
            }
        }
    }
}