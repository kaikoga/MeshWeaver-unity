using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Extensions;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public class Vertie
    {
        public readonly Matrix4x4 Translation;
        public readonly bool Culled;
        
        readonly Channel<Vector2>[] _uvs;
        public IEnumerable<Channel<Vector2>> Uvs => _uvs;

        public readonly Vector3 Vertex;
        public readonly Vector2 Uv;

        public Vertie(Vector3 vertex) : this(Matrix4x4.Translate(vertex), false, new []{ new Channel<Vector2>(Vector2.zero, 0) }) { }

        public Vertie(Matrix4x4 translation, bool culled, IEnumerable<Channel<Vector2>> uvs) : this(translation, culled, uvs.ToArray()) { } 

        public Vertie(Matrix4x4 translation, bool culled, Channel<Vector2>[] uvs)
        {
            Translation = translation;
            Culled = culled;

            _uvs = uvs;

            Vertex = new Vector3(translation.m03, translation.m13, translation.m23);
            Uv = _uvs.Value();
        }

        public bool VertexEquals(Vertie other, float sqrError = 0.000001f) => (Vertex - other.Vertex).sqrMagnitude <= sqrError;

        public Vertie MultiplyPoint(Matrix4x4 translation)
        {
            return new Vertie(translation * Translation, Culled, Uvs);
        }

        public static Vertie operator +(Vertie a, Vector3 vertex)
        {
            return new Vertie(Matrix4x4.Translate(vertex) * a.Translation, a.Culled, a.Uvs);
        }

        public static Vertie operator +(Vertie a, Vertie b)
        {
            return new Vertie(ComponentWiseAdd(a.Translation, b.Translation), a.Culled && b.Culled, a.Uvs.ZipChannels(b.Uvs, (x, y) => x + y));
        }

        public static Vertie operator -(Vertie a, Vertie b)
        {
            return new Vertie(ComponentWiseSubtract(a.Translation, b.Translation), a.Culled && b.Culled, a.Uvs.ZipChannels(b.Uvs, (x, y) => x - y));
        }

        public static Vertie operator *(Vertie a, Vertie b)
        {
            return new Vertie(a.Translation * b.Translation, a.Culled && b.Culled, a.Uvs.ZipChannels(b.Uvs, (x, y) => x + y));
        }

        public static Vertie operator /(Vertie a, Vertie b)
        {
            // I don't think this is correct, Translation part in particular, but we need the Vertex part at least
            return new Vertie(a.Translation * b.Translation.inverse, a.Culled && b.Culled, a.Uvs.ZipChannels(b.Uvs, (x, y) => x - y));
        }

        public static Vertie operator *(Vertie a, float f)
        {
            return new Vertie(ComponentWiseMultiply(a.Translation, f), a.Culled, a.Uvs.SelectChannelValues(uv => uv * f));
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
            var uvs = string.Join(", ", Uvs.Select(uv => $"[{uv.MinIndex}] : {uv.Value.x:G3}, {uv.Value.y:G3}"));
            return $"[{(Culled ? "?" : "")} {Vertex.x:G3}, {Vertex.y:G3}, {Vertex.z:G3}] ({scale.x:G3}, {scale.y:G3}, {scale.z:G3} : {angle:G3}) <{uvs}> ";
        }
    }
}