using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Extensions
{
    public static class TransformExtension
    {
        public static Matrix4x4 ToLocalMatrix(this Transform transform) => Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
    }
}