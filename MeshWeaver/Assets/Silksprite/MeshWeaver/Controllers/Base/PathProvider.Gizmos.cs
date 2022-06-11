#if UNITY_EDITOR

using System.Linq;
using Silksprite.MeshWeaver.Controllers.Paths;
using Silksprite.MeshWeaver.Models;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract partial class PathProvider
    {
        void OnDrawGizmos()
        {
            if (!(Selection.activeTransform is Transform activeTransform)) return;
            if (transform == activeTransform || transform.parent == activeTransform)
            {
                DoDrawGizmos();
            }
            if (!(activeTransform.parent is Transform activeParent)) return;
            if (transform == activeParent || transform.parent == activeParent)
            {
                DoDrawGizmos();
            }
        }

        void DoDrawGizmos()
        {
            void DrawPath(Vector3[] vertices, Color color, float width)
            {
                var oldColor = Handles.color;
                Handles.color = color;
                Handles.DrawAAPolyLine(Texture2D.whiteTexture, width, vertices);
                Handles.color = oldColor;
            }
            var containerMatrix = transform.localToWorldMatrix;

            var pathie = ToFactory().Build(LodMaskLayer.LOD0);
            var points = pathie.Vertices.Select(v => containerMatrix.MultiplyPoint3x4(v.Vertex)).ToArray();

            Gizmos.matrix = Matrix4x4.identity;
            if (this is VertexProvider)
            {
                Gizmos.DrawIcon(transform.position, "curvekeyframeweighted", false); // it works!
            }
            else
            {
                DrawPath(points, Color.black, 5f);
                DrawPath(points, Color.white, 3f);
            }
        }
    }
}

#endif
