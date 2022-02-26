using System.Linq;
using Silksprite.MeshBuilder.Models;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers
{
    [ExecuteAlways]
    public class MeshBehaviour : CustomMeshBehaviour
    {
        public MeshProvider[] meshProviders;

        protected override void OnPopulateMesh(Meshie meshie)
        {
            foreach (var meshProvider in meshProviders.Where(component => component.isActiveAndEnabled))
            {
                meshie.Concat(meshProvider.ToMeshie());
            }
        }
    }
}