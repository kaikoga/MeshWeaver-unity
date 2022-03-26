using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.DataObjects;
using Silksprite.MeshBuilder.Models.DataObjects.Extensions;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Silksprite.MeshBuilder.Controllers.Paths
{
    public class VertexProvider : PathProvider
    {
        public Vector2MuxData[] uvs;

        protected override Pathie GeneratePathie(LodMaskLayer lod)
        {
            var vertex = new Vertie(Matrix4x4.identity, !lodMask.HasLayer(lod), uvs.ToMux());
            return new Pathie(vertex);
        }
        
        #if UNITY_EDITOR
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
        #endif
    }
}