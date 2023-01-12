#if UNITY_EDITOR

using System.Linq;
using Silksprite.MeshWeaver.Controllers.Extensions;
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
            if (transform == activeTransform)
            {
                // DoDrawGizmos(true);
                return;
            }
            if (transform.parent == activeTransform)
            {
                DoDrawGizmos(false);
                return;
            }
            if (!(activeTransform.parent is Transform activeParent)) return;
            if (transform == activeParent || transform.parent == activeParent)
            {
                DoDrawGizmos(false);
            }
        }

        void OnDrawGizmosSelected()
        {
            if (!(Selection.activeTransform is Transform activeTransform)) return;
            if (transform == activeTransform)
            {
                DoDrawGizmos(true);
            }
        }

        void DoDrawGizmos(bool selected)
        {
            void DrawPath(Vector3[] vertices, Color color, float width)
            {
                var oldColor = Handles.color;
                Handles.color = color;
                Handles.DrawAAPolyLine(Texture2D.whiteTexture, width, vertices);
                Handles.color = oldColor;
            }
            var containerMatrix = transform.localToWorldMatrix * transform.ToLocalTranslation();

            var pathie = ToFactory().Build(MeshWeaverSettings.Current.CurrentLodMaskLayer);
            var points = pathie.Vertices.Select(v => containerMatrix.MultiplyPoint3x4(v.Vertex)).ToArray();

            Gizmos.matrix = Matrix4x4.identity;
            if (this is VertexProvider)
            {
                Gizmos.DrawIcon(transform.position, selected ? "curvekeyframeselected" : "curvekeyframeweighted", false);
            }
            else
            {
                if (selected)
                {
                    DrawPath(points, Color.black, 4f);
                    DrawPath(points, Color.white, 2f);
                }
                else
                {
                    DrawPath(points, Color.gray * 0.5f, 3f);
                    DrawPath(points, Color.gray * 1.5f, 1f);
                }
            }
        }
    }
}

#endif
