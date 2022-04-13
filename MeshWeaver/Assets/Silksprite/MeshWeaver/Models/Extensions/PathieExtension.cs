using System;
using System.Collections.Generic;
using System.Linq;

namespace Silksprite.MeshWeaver.Models.Extensions
{
    public static class PathieExtension
    {
        public static IEnumerable<Vertie> DedupLoop(this Pathie e)
        {
            return e.DedupLoop((a, b) => a.TranslationEquals(b));
        }

        public static IEnumerable<Vertie> DedupLoop(this Pathie e, Func<Vertie, Vertie, bool> equality)
        {
            var vertices = e.Active.Vertices;
            if (vertices.Count < 2) return vertices;
            var enumerable = equality(e.First, e.Last) ? vertices.Skip(1) : vertices;
            return enumerable.Dedup(equality);
        }

        public static IEnumerable<int> ChangingIndices(this Pathie e)
        {
            return e.ChangingIndices((a, b) => a.TranslationEquals(b));
        }

        public static IEnumerable<int> ChangingIndices(this Pathie e, Func<Vertie, Vertie, bool> equality)
        {
            return e.Active.Vertices.Pairwise((a, b) => (a, b))
                .Select((ab, i) => (ab.a, ab.b, i))
                .Where(abi => !equality(abi.a, abi.b))
                .Select(abi => abi.i);
        }

        static IEnumerable<float> ToLengths(this Pathie pathie)
        {
            return pathie.Vertices.Pairwise((a, b) => (b.Vertex - a.Vertex).magnitude);
        }

        public static IEnumerable<float> ToNetLengths(this Pathie pathie)
        {
            return new [] { 0f }.Concat(pathie.ToLengths().Integral());
        }

        public static IEnumerable<float> ToNetProportions(this Pathie pathie)
        {
            var lengths = pathie.ToNetLengths().ToArray();
            return lengths.Select(v => v / lengths[lengths.Length - 1]);
        }
    }
}