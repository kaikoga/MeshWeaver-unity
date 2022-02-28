using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public readonly struct Vertie
    {
        public static readonly Vertie Empty = new Vertie();

        public readonly Vector3 Vertex;
        public readonly Vector2 Uv;

        public Vertie(Vector3 vertex, Vector2 uv)
        {
            Vertex = vertex;
            Uv = uv;
        }

        public Vertie MultiplyPoint(Matrix4x4 matrix)
        {
            return new Vertie(matrix.MultiplyPoint(Vertex), Uv);
        }

        public static Vertie operator +(Vertie a, Vertie b)
        {
            return new Vertie(a.Vertex + b.Vertex, a.Uv + b.Uv);
        }
    }
}