using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public class Vertie
    {
        public readonly Vector3 Vertex;
        public readonly Vector2 Uv;
        public readonly Matrix4x4 Translation;
        public readonly bool Culled;

        public Vertie(Vector3 vertex) : this(vertex, Vector2.zero, Matrix4x4.Translate(vertex), false) { }

        public Vertie(Matrix4x4 translation, Vector2 uv, bool culled) : this(new Vector3(translation.m03, translation.m13, translation.m23), uv, translation, culled) { }

        Vertie(Vector3 vertex, Vector2 uv, Matrix4x4 translation, bool culled)
        {
            Vertex = vertex;
            Uv = uv;
            Translation = translation;
            Culled = culled;
        }

        public bool VertexEquals(Vertie other, float sqrError = 0.000001f) => (Vertex - other.Vertex).sqrMagnitude <= sqrError;

        public Vertie MultiplyPoint(Matrix4x4 translation)
        {
            return new Vertie(translation.MultiplyPoint(Vertex), Uv, translation * Translation, Culled);
        }

        public static Vertie operator +(Vertie a, Vector3 vertex)
        {
            return new Vertie(a.Vertex + vertex, a.Uv, Matrix4x4.Translate(vertex) * a.Translation, a.Culled);
        }

        public static Vertie operator +(Vertie a, Vertie b)
        {
            return new Vertie(a.Vertex + b.Vertex, a.Uv + b.Uv, ComponentWiseAdd(a.Translation, b.Translation), a.Culled && b.Culled);
        }

        public static Vertie operator -(Vertie a, Vertie b)
        {
            return new Vertie(a.Vertex - b.Vertex, a.Uv - b.Uv, ComponentWiseSubtract(a.Translation, b.Translation), a.Culled && b.Culled);
        }

        public static Vertie operator *(Vertie a, Vertie b)
        {
            return new Vertie(a.Translation.MultiplyPoint(b.Vertex), a.Uv + b.Uv, a.Translation * b.Translation, a.Culled && b.Culled);
        }

        public static Vertie operator /(Vertie a, Vertie b)
        {
            // I don't think this is correct, Translation part in particular, but we need the Vertex part at least
            return new Vertie(b.Translation.inverse.MultiplyPoint(a.Vertex), a.Uv - b.Uv, a.Translation * b.Translation.inverse, a.Culled && b.Culled);
        }

        public static Vertie operator *(Vertie a, float f)
        {
            return new Vertie(a.Vertex * f, a.Uv * f, ComponentWiseMultiply(a.Translation, f), a.Culled);
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

        static Matrix4x4 ComponentWiseMultiply(Matrix4x4 a, float f)
        {
            var result = new Matrix4x4();
            for (var i = 0; i < 16; i++) result[i] = a[i] * f;
            return result;
        }

        public override string ToString()
        {
            var translation = Translation;
            var scale = translation.lossyScale;
            translation.rotation.ToAngleAxis(out var angle, out _);
            return $"[{(Culled ? "?" : "")} {Vertex.x:G3}, {Vertex.y:G3}, {Vertex.z:G3}] <{Uv.x:G3}, {Uv.y:G3}> ({scale.x:G3}, {scale.y:G3}, {scale.z:G3} : {angle:G3})";
        }
    }
}