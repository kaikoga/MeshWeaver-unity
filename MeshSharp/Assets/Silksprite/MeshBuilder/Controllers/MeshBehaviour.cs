using System.Collections.Generic;
using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers
{
    [ExecuteAlways]
    public class MeshBehaviour : CustomMeshBehaviour
    {
        public List<MeshProvider> meshProviders = new List<MeshProvider>();

        protected override void OnPopulateMesh(Meshie meshie)
        {
            CollectMeshies(meshProviders, meshie);
        }
    }
}