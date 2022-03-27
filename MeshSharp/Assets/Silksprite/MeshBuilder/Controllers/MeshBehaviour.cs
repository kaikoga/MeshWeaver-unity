using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers
{
    [ExecuteAlways]
    public class MeshBehaviour : CustomMeshBehaviour
    {
        protected override Meshie OnPopulateMesh(LodMaskLayer lodMask)
        {
            return CollectMeshies(this.GetComponentsInDirectChildren<MeshProvider>(), lodMask);
        }
    }
}