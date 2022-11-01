using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Extensions
{
    public static class SerializedPropertyExtension
    {
        public static Matrix4x4 GetMatrix4x4Value(this SerializedProperty property)
        {
            property = property.Copy();
            property.NextVisible(true);
            var e00 = property.floatValue;
            property.NextVisible(false);
            var e01 = property.floatValue;
            property.NextVisible(false);
            var e02 = property.floatValue;
            property.NextVisible(false);
            var e03 = property.floatValue;
            property.NextVisible(false);
            var e10 = property.floatValue;
            property.NextVisible(false);
            var e11 = property.floatValue;
            property.NextVisible(false);
            var e12 = property.floatValue;
            property.NextVisible(false);
            var e13 = property.floatValue;
            property.NextVisible(false);
            var e20 = property.floatValue;
            property.NextVisible(false);
            var e21 = property.floatValue;
            property.NextVisible(false);
            var e22 = property.floatValue;
            property.NextVisible(false);
            var e23 = property.floatValue;
            property.NextVisible(false);
            var e30 = property.floatValue;
            property.NextVisible(false);
            var e31 = property.floatValue;
            property.NextVisible(false);
            var e32 = property.floatValue;
            property.NextVisible(false);
            var e33 = property.floatValue;
            return new Matrix4x4(
                new Vector4(e00, e01, e02, e03),
                new Vector4(e10, e11, e12, e13),
                new Vector4(e20, e21, e22, e23),
                new Vector4(e30, e31, e32, e33));
        }

        public static void SetMatrix4x4Value(this SerializedProperty property, Matrix4x4 matrix4x4)
        {
            property = property.Copy();
            property.NextVisible(true);
            property.floatValue = matrix4x4.m00;
            property.NextVisible(false);
            property.floatValue = matrix4x4.m10;
            property.NextVisible(false);
            property.floatValue = matrix4x4.m20;
            property.NextVisible(false);
            property.floatValue = matrix4x4.m30;
            property.NextVisible(false);
            property.floatValue = matrix4x4.m01;
            property.NextVisible(false);
            property.floatValue = matrix4x4.m11;
            property.NextVisible(false);
            property.floatValue = matrix4x4.m21;
            property.NextVisible(false);
            property.floatValue = matrix4x4.m31;
            property.NextVisible(false);
            property.floatValue = matrix4x4.m02;
            property.NextVisible(false);
            property.floatValue = matrix4x4.m12;
            property.NextVisible(false);
            property.floatValue = matrix4x4.m22;
            property.NextVisible(false);
            property.floatValue = matrix4x4.m32;
            property.NextVisible(false);
            property.floatValue = matrix4x4.m03;
            property.NextVisible(false);
            property.floatValue = matrix4x4.m13;
            property.NextVisible(false);
            property.floatValue = matrix4x4.m23;
            property.NextVisible(false);
            property.floatValue = matrix4x4.m33;
        }
    }
}