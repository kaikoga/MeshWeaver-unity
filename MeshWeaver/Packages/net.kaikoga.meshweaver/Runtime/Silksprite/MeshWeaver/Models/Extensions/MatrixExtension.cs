using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Extensions
{
    public static class MatrixExtension
    {
        public static Vector3 GetPosition(this Matrix4x4 m) => new Vector3(m.m03, m.m13, m.m23); // (Vector3)m.GetColumn(3);

        public static Matrix4x4 WithPosition(this Matrix4x4 m, Vector3 t)
        {
            m.m03 = t.x;
            m.m13 = t.y;
            m.m23 = t.z;
            return m;
        }
    }
}