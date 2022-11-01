using System;
using System.Linq;
using JetBrains.Annotations;
using Silksprite.MeshWeaver.Models.Extensions;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models
{
    public sealed class Vertie : IEquatable<Vertie>
    {
        public readonly Matrix4x4 Translation;
        
        public readonly Mux<Vector2> Uvs;

        public Vector3 Vertex => Translation.GetPosition();
        public Vector2 Uv => Uvs.Value;

        public Vertie(Vector3 vertex) : this(Matrix4x4.Translate(vertex), Mux.Single(Vector2.zero)) { }
        public Vertie(Matrix4x4 translation) : this(translation, Mux.Single(Vector2.zero)) { }

        public Vertie(Matrix4x4 translation, [NotNull] Mux<Vector2> uvs)
        {
            Translation = translation;
            Uvs = uvs;
        }

        public static readonly Vertie Identity = new Vertie(Matrix4x4.identity, Mux.Empty<Vector2>());

        public bool VertexEquals(Vertie other, float sqrError = 0.000001f) => (Vertex - other.Vertex).sqrMagnitude <= sqrError;
        public bool TranslationEquals(Vertie other, float error = 0.000001f)
        {
            for (var i = 0; i < 16; i++)
            {
                var d = Translation[i] - other.Translation[i];
                if (d > error || d < -error) return false;
            }
            return true;
        }

        public Vertie MultiplyPoint(Matrix4x4 translation)
        {
            return new Vertie(translation * Translation, Uvs);
        }

        public static Vertie operator +(Vertie a, Vector3 vertex)
        {
            return new Vertie(Matrix4x4.Translate(vertex) * a.Translation, a.Uvs);
        }

        public static Vertie operator +(Vertie a, Vertie b)
        {
            // Use this for blending only. Perhaps another type?
            return new Vertie(ComponentWiseAdd(a.Translation, b.Translation), a.Uvs.ZipMux(b.Uvs, (x, y) => x + y));
        }

        public static Vertie operator -(Vertie a, Vertie b)
        {
            // Use this for blending only. Perhaps another type?
            return new Vertie(ComponentWiseSubtract(a.Translation, b.Translation), a.Uvs.ZipMux(b.Uvs, (x, y) => x - y));
        }

        public static Vertie operator *(Vertie a, Vertie b)
        {
            return new Vertie(a.Translation * b.Translation, a.Uvs.ZipMux(b.Uvs, (x, y) => x + y));
        }

        public static Vertie operator /(Vertie a, Vertie b)
        {
            // I don't think this is correct, Translation part in particular, but we need the Vertex part at least
            return new Vertie(a.Translation * b.Translation.inverse, a.Uvs.ZipMux(b.Uvs, (x, y) => x - y));
        }

        public static Vertie operator *(Vertie a, float f)
        {
            return new Vertie(ComponentWiseMultiply(a.Translation, f), a.Uvs.SelectMuxValues(uv => uv * f));
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
            var uvs = string.Join(", ", Uvs.Select(uv => $"[{uv.Channel}] : {uv.Value.x:G3}, {uv.Value.y:G3}"));
            return $"[{Vertex.x:G3}, {Vertex.y:G3}, {Vertex.z:G3}] ({scale.x:G3}, {scale.y:G3}, {scale.z:G3} : {angle:G3}) <{uvs}> ";
        }

        #region IEquatable<Vertie>
        public bool Equals(Vertie other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Translation.Equals(other.Translation) && Uvs.Equals(other.Uvs);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Vertie other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Translation.GetHashCode() * 397) ^ Uvs.GetHashCode();
            }
        }

        public static bool operator ==(Vertie left, Vertie right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Vertie left, Vertie right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}