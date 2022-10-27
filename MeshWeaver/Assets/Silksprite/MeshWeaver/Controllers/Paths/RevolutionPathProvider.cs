using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Models.Paths;
using UnityEngine.Serialization;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    public class RevolutionPathProvider : PathProvider
    {
        public float min = 0f;
        public float max = 360f;
        public float radius = 0f;
        [FormerlySerializedAs("steps")]
        public int subdivision = 16;
        public RevolutionPathieFactory.Axis axis = RevolutionPathieFactory.Axis.X;
        public bool isLoop = true;

        protected override IPathieFactory CreateFactory()
        {
            return new RevolutionPathieFactory(min, max, radius, subdivision, axis, isLoop);
        }
    }
}