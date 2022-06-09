using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using Silksprite.MeshWeaver.Models.Utils;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Modifiers
{
    public class UvProjector : VertwiseModifierBase
    {
        readonly ProjectionKind _projection;
        readonly Matrix4x4 _translation;
        readonly ProjectionAxisKind _axisX;
        readonly ProjectionAxisKind _axisY;
        readonly Rect _remapArea;
        readonly int _uvChannel;

        static Vector3 VectorAxis(ProjectionAxisKind axis)
        {
            switch (axis)
            {
                case ProjectionAxisKind.XPlus:
                    return new Vector3(1f, 0f, 0f);
                case ProjectionAxisKind.XMinus:
                    return new Vector3(-1f, 0f, 0f);
                case ProjectionAxisKind.YPlus:
                    return new Vector3(0f, 1f, 0f);
                case ProjectionAxisKind.YMinus:
                    return new Vector3(0f, -1f, 0f);
                case ProjectionAxisKind.ZPlus:
                    return new Vector3(0f, 0f, 1f);
                case ProjectionAxisKind.ZMinus:
                    return new Vector3(0f, 0f, -1f);
                default:
                    return new Vector3(1f, 0f, 0f);
            }
        }

        Vector3 VectorX => VectorAxis(_axisX);
        Vector3 VectorY => VectorAxis(_axisY);
            
        public UvProjector(ProjectionKind projection, Matrix4x4 translation, ProjectionAxisKind axisX, ProjectionAxisKind axisY, Rect remapArea, int uvChannel)
        {
            _projection = projection;
            _translation = translation;
            _axisX = axisX;
            _axisY = axisY;
            _remapArea = remapArea;
            _uvChannel = uvChannel;
        }

        protected override IEnumerable<Vertie> Modify(IEnumerable<Vertie> vertices)
        {
            Matrix4x4 CreateMatrix(Vector3 pivot, Vector3 invOneX, Vector3 invOneY)
            {
                var matrix = new Matrix4x4
                {
                    m00 = invOneX.x * _remapArea.width,
                    m10 = invOneY.x * _remapArea.height,
                    m20 = 0f,
                    m30 = 0f,
                    m01 = invOneX.y * _remapArea.width,
                    m11 = invOneY.y * _remapArea.height,
                    m21 = 0f,
                    m31 = 0f,
                    m02 = invOneX.z * _remapArea.width,
                    m12 = invOneY.z * _remapArea.height,
                    m22 = 0f,
                    m32 = 0f,
                    m03 = -pivot.x + _remapArea.x,
                    m13 = -pivot.y + _remapArea.y,
                    m23 = -pivot.z,
                    m33 = 1f
                };
                return matrix;
            }

            Matrix4x4 AutoProjection()
            {
                vertices = vertices.ToArray();
                var bounds = BoundsUtil.CalculateBounds(vertices.Select(v => v.Vertex));
                var pivot = bounds.min;
                var size = bounds.size;
                var invSize = new Vector3(size.x == 0f ? 1f : 1f / size.x, size.y == 0f ? 1f : 1f / size.y, size.z == 0f ? 1f : 1f / size.z);
                var oneX = VectorX; 
                var oneY = VectorY;
                var invOneX = new Vector3(invSize.x * oneX.x, invSize.y * oneX.y, invSize.z * oneX.z);
                var invOneY = new Vector3(invSize.x * oneY.x, invSize.y * oneY.y, invSize.z * oneY.z);
                pivot.Scale(invSize);
                switch (_projection)
                {
                    case ProjectionKind.Expand:
                        var magMin = Mathf.Min(invOneX.magnitude, invOneY.magnitude);
                        invOneX *= magMin / invOneX.magnitude;
                        invOneY *= magMin / invOneY.magnitude;
                        break;
                    case ProjectionKind.Shrink:
                        var magMax = Mathf.Max(invOneX.magnitude, invOneY.magnitude);
                        invOneX *= magMax / invOneX.magnitude;
                        invOneY *= magMax / invOneY.magnitude;
                        break;
                }

                return CreateMatrix(pivot, invOneX, invOneY);
            }

            Matrix4x4 AbsoluteProjection() => CreateMatrix(Vector3.zero, VectorX, VectorY);

            Matrix4x4 translation;
            switch (_projection)
            {
                case ProjectionKind.Translation:
                    translation = _translation;
                    break;
                case ProjectionKind.Absolute:
                    translation = AbsoluteProjection();
                    break;
                case ProjectionKind.Normalized:
                case ProjectionKind.Expand:
                case ProjectionKind.Shrink:
                    translation = AutoProjection();
                    break;
                default:
                    translation = Matrix4x4.identity;
                    break;
            }
            return vertices.Select(vertie => vertie.AddUv(translation.MultiplyPoint3x4(vertie.Vertex), _uvChannel));
        }

        public enum ProjectionKind
        {
            Translation = 0,
            Absolute = 1,
            Normalized = 2,
            Expand = 3,
            Shrink = 4,
        }

        public enum ProjectionAxisKind
        {
            XPlus = 0,
            XMinus = 1,
            YPlus = 2,
            YMinus = 3,
            ZPlus = 4,
            ZMinus = 5,
        }
    }
}