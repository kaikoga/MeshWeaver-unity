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
            return e.DedupLoop((a, b) => Equals(a, b));
        }

        public static IEnumerable<Vertie> DedupLoop(this Pathie e, Func<Vertie, Vertie, bool> equality)
        {
            return equality(e.First, e.Last) ? e.Skip(1).Dedup(equality) : e.Dedup(equality);
        }
    }
}