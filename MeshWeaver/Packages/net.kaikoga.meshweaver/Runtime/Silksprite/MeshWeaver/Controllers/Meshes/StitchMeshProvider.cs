using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Paths;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class StitchMeshProvider : MeshProvider
    {
        protected override bool RefreshAlways => true;

        public PathProvider pathProviderA;
        public PathProvider pathProviderB;

        public Material material;

        public IPathieFactory LastPathieA { get; private set; }
        public IPathieFactory LastPathieB { get; private set; }

        protected override IMeshieFactory CreateFactory(IMeshContext context)
        {
            context.GetMaterialIndex(material);
            LastPathieA = pathProviderA.CollectPathie(context);
            LastPathieB = pathProviderB.CollectPathie(context);
            return new StitchMeshieFactory(LastPathieA, LastPathieB, material);
        }
    }
}