using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Models;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers
{
    [ExecuteAlways]
    public class MeshBehaviour : CustomMeshBehaviour
    {
        protected override Meshie OnPopulateMesh(LodMaskLayer lod)
        {
            return CollectMeshies(this.GetComponentsInDirectChildren<MeshProvider>()).Build(lod);
        }
    }
}