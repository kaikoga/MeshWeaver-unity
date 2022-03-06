using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public readonly struct Vertie
    {
        public readonly Vector3 Vertex;
        public readonly Vector2 Uv;
        public readonly Matrix4x4 Translation;

        public Vertie(Vector3 vertex, Vector2 uv) : this(vertex, uv, Matrix4x4.Translate(vertex)) { }
        public Vertie(Matrix4x4 translation, Vector2 uv) : this(new Vector3(translation.m03, translation.m13, translation.m23), uv, translation) { }

        Vertie(Vector3 vertex, Vector2 uv, Matrix4x4 translation)
        {
            Vertex = vertex;
            Uv = uv;
            Translation = translation;
        }

        public Vertie MultiplyPoint(Matrix4x4 translation)
        {
            return new Vertie(translation.MultiplyPoint(Vertex), Uv, translation * Translation);
        }

        public static Vertie operator +(Vertie a, Vertie b)
        {
            return new Vertie(a.Vertex + b.Vertex, a.Uv + b.Uv, ComponentWiseAdd(a.Translation, b.Translation));
        }

        public static Vertie operator -(Vertie a, Vertie b)
        {
            return new Vertie(a.Vertex - b.Vertex, a.Uv - b.Uv, ComponentWiseSubtract(a.Translation, b.Translation));
        }

        public static Vertie operator *(Vertie a, Vertie b)
        {
            return new Vertie(a.Translation.MultiplyPoint(b.Vertex), a.Uv + b.Uv, a.Translation * b.Translation);
        }

        static Matrix4x4 ComponentWiseAdd(Matrix4x4 a, Matrix4x4 b)
        {
            var result = new Matrix4x4();
            for (var i = 0; i < 16; i++) result[i] = a[i] + b[i];
            return result;
        }
        static Matrix4x4 ComponentWiseSubtract(Matrix4x4 a, Matrix4x4 b)
        {
            var result = new Matrix4x4();
            for (var i = 0; i < 16; i++) result[i] = a[i] - b[i];
            return result;
        }

        public override string ToString()
        {
            var translation = Translation;
            var scale = translation.lossyScale;
            translation.rotation.ToAngleAxis(out var angle, out _);
            return $"[{Vertex.x:G3}, {Vertex.y:G3}, {Vertex.z:G3}] <{Uv.x:G3}, {Uv.y:G3}> ({scale.x:G3}, {scale.y:G3}, {scale.z:G3} : {angle:G3})";
        }
    }
}