using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    public class RevolutionPathProvider : PathProvider
    {
        public float min = 0f;
        public float max = 360f;
        public float radius = 0f;
        public int steps = 16;
        public RevolutionPathieFactory.Axis axis = RevolutionPathieFactory.Axis.X;
        public bool isLoop = true;

        protected override IPathieFactory CreateFactory()
        {
            return new RevolutionPathieFactory(min, max, radius, steps, axis, isLoop);
        }
    }
}