using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    public class VertexProvider : PathProvider
    {
        public Vector2 uv;

        protected override Pathie GeneratePathie()
        {
            return new Pathie { new Vertie(new Vector3(), uv, Matrix4x4.identity) };
        }
        
        void OnDrawGizmos()
        {
            var activeTransform = Selection.activeTransform;
            if (activeTransform == null) return;
            var activeParent = activeTransform.parent;
            if (transform == activeTransform || transform.IsChildOf(activeParent ? activeParent : activeTransform))
            {
                Gizmos.DrawIcon(transform.position, "curvekeyframeweighted", false); // it works!
            }
        }
    }
}