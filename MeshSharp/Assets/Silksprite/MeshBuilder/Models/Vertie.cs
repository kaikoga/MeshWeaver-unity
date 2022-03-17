using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public class Vertie
    {
        public readonly Matrix4x4 Translation;
        public readonly bool Culled;
        
        public readonly Vector2 Uv;

        public readonly Vector3 Vertex;

        public Vertie(Vector3 vertex) : this(Matrix4x4.Translate(vertex), false, Vector2.zero) { }

        public Vertie(Matrix4x4 translation, bool culled, Vector2 uv)
        {
            Translation = translation;
            Culled = culled;

            Uv = uv;

            Vertex = new Vector3(translation.m03, translation.m13, translation.m23);
        }

        public bool VertexEquals(Vertie other, float sqrError = 0.000001f) => (Vertex - other.Vertex).sqrMagnitude <= sqrError;

        public Vertie MultiplyPoint(Matrix4x4 translation)
        {
            return new Vertie(translation * Translation, Culled, Uv);
        }

        public static Vertie operator +(Vertie a, Vector3 vertex)
        {
            return new Vertie(Matrix4x4.Translate(vertex) * a.Translation, a.Culled, a.Uv);
        }

        public static Vertie operator +(Vertie a, Vertie b)
        {
            return new Vertie(ComponentWiseAdd(a.Translation, b.Translation), a.Culled && b.Culled, a.Uv + b.Uv);
        }

        public static Vertie operator -(Vertie a, Vertie b)
        {
            return new Vertie(ComponentWiseSubtract(a.Translation, b.Translation), a.Culled && b.Culled, a.Uv - b.Uv);
        }

        public static Vertie operator *(Vertie a, Vertie b)
        {
            return new Vertie(a.Translation * b.Translation, a.Culled && b.Culled, a.Uv + b.Uv);
        }

        public static Vertie operator /(Vertie a, Vertie b)
        {
            // I don't think this is correct, Translation part in particular, but we need the Vertex part at least
            return new Vertie(a.Translation * b.Translation.inverse, a.Culled && b.Culled, a.Uv - b.Uv);
        }

        public static Vertie operator *(Vertie a, float f)
        {
            return new Vertie(ComponentWiseMultiply(a.Translation, f), a.Culled, a.Uv * f);
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