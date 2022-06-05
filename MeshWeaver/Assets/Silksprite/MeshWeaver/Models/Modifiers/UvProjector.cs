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
        readonly ProjectionAxisKind _axis;
        readonly int _uvChannel;

        public UvProjector(ProjectionKind projection, Matrix4x4 translation, ProjectionAxisKind axis, int uvChannel)
        {
            _projection = projection;
            _translation = translation;
            _axis = axis;
            _uvChannel = uvChannel;
        }

        protected override IEnumerable<Vertie> Modify(IEnumerable<Vertie> vertices)
        {
            Matrix4x4 CreateMatrix(Vector3 pivot, Vector3 invOneX, Vector3 invOneY)
            {
                var matrix = new Matrix4x4
                {
                    m00 = invOneX.x,
                    m10 = invOneY.x,
                    m20 = 0f,
                    m30 = 0f,
                    m01 = invOneX.y,
                    m11 = invOneY.y,
                    m21 = 0f,
                    m31 = 0f,
                    m02 = invOneX.z,
                    m12 = invOneY.z,
                    m22 = 0f,
                    m32 = 0f,
                    m03 = -pivot.x,
                    m13 = -pivot.y,
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
                var invSize = new Vector3(1f / size.x, 1f / size.y, 1f / size.z); 
                pivot.Scale(invSize);
                switch (_axis)
                {
                    case ProjectionAxisKind.XY:
                        return CreateMatrix(pivot, new Vector3(invSize.x, 0f, 0f), new Vector3(0, invSize.y, 0f));
                    case ProjectionAxisKind.XZ:
                        return CreateMatrix(pivot, new Vector3(invSize.x, 0f, 0f), new Vector3(0, 0f, invSize.z));
                    case ProjectionAxisKind.YX:
                        return CreateMatrix(pivot, new Vector3(0f, invSize.y, 0f), new Vector3(invSize.x, 0f, 0f));
                    case ProjectionAxisKind.YZ:
                        return CreateMatrix(pivot, new Vector3(0f, invSize.y, 0f), new Vector3(0f, 0f, invSize.z));
                    case ProjectionAxisKind.ZX:
                        return CreateMatrix(pivot, new Vector3(0f, 0f, invSize.z), new Vector3(invSize.x, 0f, 0f));
                    case ProjectionAxisKind.ZY:
                        return CreateMatrix(pivot, new Vector3(0f, 0f, invSize.z), new Vector3(0, invSize.y, 0f));
                    default:
                        return Matrix4x4.identity;
                }
            }

            Matrix4x4 AbsoluteProjection()
            {
                switch (_axis)
                {
                    case ProjectionAxisKind.XY:
                        return CreateMatrix(Vector3.zero, new Vector3(1f, 0f, 0f), new Vector3(0, 1f, 0f));
                    case ProjectionAxisKind.XZ:
                        return CreateMatrix(Vector3.zero, new Vector3(1f, 0f, 0f), new Vector3(0, 0f, 1f));
                    case ProjectionAxisKind.YX:
                        return CreateMatrix(Vector3.zero, new Vector3(0f, 1f, 0f), new Vector3(1f, 0f, 0f));
                    case ProjectionAxisKind.YZ:
                        return CreateMatrix(Vector3.zero, new Vector3(0f, 1f, 0f), new Vector3(0f, 0f, 1f));
                    case ProjectionAxisKind.ZX:
                        return CreateMatrix(Vector3.zero, new Vector3(0f, 0f, 1f), new Vector3(1f, 0f, 0f));
                    case ProjectionAxisKind.ZY:
                        return CreateMatrix(Vector3.zero, new Vector3(0f, 0f, 1f), new Vector3(0, 1f, 0f));
                    default:
                        return Matrix4x4.identity;
                }
            }

            var translation = _translation;
            switch (_projection)
            {
                case ProjectionKind.Translation:
                    translation = _translation;
                    break;
                case ProjectionKind.Normalized:
                    translation = AutoProjection();
                    break;
                case ProjectionKind.Absolute:
                    translation = AbsoluteProjection();
                    break;
                default:
                    break;
            }
            return vertices.Select(vertie => vertie.AddUv(translation.MultiplyPoint3x4(vertie.Vertex), _uvChannel));
        }

        public enum ProjectionKind
        {
            Translation = 0,
            Normalized = 1,
            Absolute = 2
        }

        public enum ProjectionAxisKind
        {
            XY = 0,
            XZ = 1,
            YX = 2,
            YZ = 3,
            ZX = 4,
            ZY = 5,
        }
    }
}