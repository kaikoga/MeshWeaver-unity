using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Extensions
{
    public static class PathieExtension
    {
        public static IEnumerable<Vertie> DedupLoop(this Pathie e)
        {
            return e.DedupLoop((a, b) => a.TranslationEquals(b));
        }

        public static IEnumerable<Vertie> DedupLoop(this Pathie e, Func<Vertie, Vertie, bool> equality)
        {
            return equality(e.First, e.Last) ? e.Active.Vertices.Skip(1).Dedup(equality) : e.Active.Vertices.Dedup(equality);
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
    }
}