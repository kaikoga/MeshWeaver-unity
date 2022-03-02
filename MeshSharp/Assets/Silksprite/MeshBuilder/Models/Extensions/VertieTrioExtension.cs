using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Extensions
{
    public static class VertieTrioExtensions
    {
        public static Matrix4x4 Tangent(this Trio<Vertie> trio)
        {
            var vector = trio.Next.Vertex - trio.Prev.Vertex;
            vector.y = 0;
            vector.Normalize();
            return Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.right, vector));
        }
    }
}