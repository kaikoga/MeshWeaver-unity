using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class StitchMeshProvider : MeshProvider
    {
        public PathProvider pathProviderA;
        public PathProvider pathProviderB;

        public int materialIndex;

        public IPathieFactory LastPathieA { get; private set; }
        public IPathieFactory LastPathieB { get; private set; }

        protected override IMeshieFactory CreateFactory(IMeshContext context)
        {
            LastPathieA = CollectPathie(pathProviderA);
            LastPathieB = CollectPathie(pathProviderB);
            return new StitchMeshieFactory(LastPathieA, LastPathieB, materialIndex);
        }
    }
}