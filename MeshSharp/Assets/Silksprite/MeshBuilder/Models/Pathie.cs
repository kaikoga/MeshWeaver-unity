using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models.Base;

namespace Silksprite.MeshBuilder.Models
{
    public class Pathie
    {
        public readonly List<Vertie> Vertices;

        public Pathie Active => new Pathie(ActiveVertices());

        IEnumerable<Vertie> ActiveVertices()
        {
            for (var index = 0; index < Vertices.Count; index++)
            {
                if (index == 0 || index == Vertices.Count - 1) yield return Vertices[index];
                var v = Vertices[index];
                if (!v.Culled) yield return v;
            }
        }

        public Vertie First => Vertices.Count > 0 ? Vertices[0] : default;
        public Vertie Last => Vertices.Count > 0 ? Vertices[Vertices.Count - 1] : default;
        
        public Vertie Diff => Last / First;

        Pathie(List<Vertie> vertices)
        {
            Vertices = vertices;
        }

        Pathie() : this(new List<Vertie>()) { }

        public Pathie(Vertie soleVertex) : this(new List<Vertie> { soleVertex }) { }

        public Pathie(IEnumerable<Vertie> vertices) : this(vertices.ToList()) { }

        public Pathie Modify(Func<Vertie, int, Vertie> modifier)
        {
            var result = new Pathie();
            foreach (var (vertie, i) in Vertices.Select((vertie, i) => (vertie, i)))
            {
                result.Vertices.Add(modifier(vertie, i));
            }
            return result;
        }

        public Pathie Apply(IPathieModifier modifier) => modifier.Modify(this);

        public override string ToString()
        {
            return $"V[{Vertices.Count}]";
        }

        public string Dump()
        {
            var vertices = string.Join("\n", Vertices.Select(v => v.ToString()));
            return $"V[{Vertices.Count}]\n{vertices}";
        }
        
        public static Pathie Empty() => new Pathie();
        public static PathieBuilder Builder() => new PathieBuilder();
    }
}