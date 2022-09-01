using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

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
            var enumerable = !e.isLoop && equality(e.First, e.Last) ? vertices.Skip(1) : vertices;
            return enumerable.Dedup(equality);
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