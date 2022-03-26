using System.Collections.Generic;
using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers
{
    [ExecuteAlways]
    public class MeshBehaviour : CustomMeshBehaviour, ICompositeGeometry<MeshProvider>
    {
        public List<MeshProvider> meshProviders = new List<MeshProvider>();

        public List<MeshProvider> PrimaryElements { set => meshProviders = value; }

        protected override Meshie OnPopulateMesh(LodMaskLayer lodMask)
        {
            return CollectMeshies(meshProviders, lodMask);
        }
    }
}