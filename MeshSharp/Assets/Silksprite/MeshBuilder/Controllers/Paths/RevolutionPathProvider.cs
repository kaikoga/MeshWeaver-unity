using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Paths;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    public class RevolutionPathProvider : PathProvider
    {
        public float min = 0f;
        public float max = 360f;
        public float radius = 0f;
        public int steps = 16;
        public RevolutionPathieFactory.Axis axis = RevolutionPathieFactory.Axis.X;

        protected override IPathieFactory CreateFactory(LodMaskLayer lod)
        {
            return new RevolutionPathieFactory(min, max, radius, steps, axis);
        }
    }
}