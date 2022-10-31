using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Models.Paths;
using UnityEngine;
using UnityEngine.Serialization;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    public class RevolutionPathProvider : PathProvider
    {
        [SerializeField] [HideInInspector] public bool hasLegacyRadius = true;

        public float min = 0f;
        public float max = 360f;
        [FormerlySerializedAs("radius")]
        [SerializeField] [HideInInspector] public float legacyRadius = 1f;
        [FormerlySerializedAs("steps")]
        public int subdivision = 16;
        public RevolutionPathieFactory.Axis axis = RevolutionPathieFactory.Axis.Y;
        public bool isLoop = true;

        public Vector3 vector = Vector3.right;

        protected override IPathieFactory CreateFactory()
        {
            if (hasLegacyRadius)
            {
                switch (axis)
                {
                    case RevolutionPathieFactory.Axis.X:
                        vector = Vector3.up * legacyRadius;
                        break;
                    case RevolutionPathieFactory.Axis.Y:
                        vector = Vector3.forward * legacyRadius;
                        break;
                    case RevolutionPathieFactory.Axis.Z:
                        vector = Vector3.right * legacyRadius;
                        break;
                }
                hasLegacyRadius = false;
            }
            return new RevolutionPathieFactory(min, max, vector, subdivision, axis, isLoop);
        }
    }
}