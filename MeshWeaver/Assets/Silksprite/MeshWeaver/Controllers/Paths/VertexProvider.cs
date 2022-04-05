using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.DataObjects.Extensions;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Models.DataObjects;
using Silksprite.MeshWeaver.Models.Paths;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    public class VertexProvider : PathProvider
    {
        public Vector2MuxData[] uvs;

        protected override IPathieFactory CreateFactory()
        {
            return new VertexFactory(lodMask, uvs.ToMux());
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