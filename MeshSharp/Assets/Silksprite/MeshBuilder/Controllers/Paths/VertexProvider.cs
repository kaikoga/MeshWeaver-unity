using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    public class VertexProvider : PathProvider
    {
        public Vector2 uv;

        protected override Pathie GeneratePathie(LodMask lod)
        {
            var pathie = new Pathie();
            pathie.Vertices.Add(new Vertie(Matrix4x4.identity, !lodMask.HasFlag(lod), uv));
            return pathie;
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