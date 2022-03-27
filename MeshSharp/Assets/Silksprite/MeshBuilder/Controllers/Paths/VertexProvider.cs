using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.DataObjects;
using Silksprite.MeshBuilder.Models.DataObjects.Extensions;
using Silksprite.MeshBuilder.Models.Paths;
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
            return new VertexFactory(lodMask, uvs.ToMux()).Build(lod);
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