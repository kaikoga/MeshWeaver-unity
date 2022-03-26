using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models.Base;

namespace Silksprite.MeshBuilder.Models
{
    public class Pathie
    {
        public readonly Vertie[] Vertices;

        public Pathie Active => new Pathie(ActiveVertices());

        IEnumerable<Vertie> ActiveVertices()
        {
            return Vertices.Where((v, index) => index == 0 || index == Vertices.Length - 1 || !v.Culled);
        }

        public Vertie First => Vertices.Length > 0 ? Vertices[0] : default;
        public Vertie Last => Vertices.Length > 0 ? Vertices[Vertices.Length - 1] : default;
        
        public Vertie Diff => Last / First;

        Pathie(Vertie[] vertices)
        {
            Vertices = vertices;
        }

        Pathie() : this(Array.Empty<Vertie>()) { }

        public Pathie(Vertie soleVertex) : this(new List<Vertie> { soleVertex }) { }

        public Pathie(IEnumerable<Vertie> vertices) : this(vertices.ToArray()) { }

        public Pathie Modify(Func<Vertie, int, Vertie> modifier)
        {
            var result = new PathieBuilder();
            foreach (var (vertie, i) in Vertices.Select((vertie, i) => (vertie, i)))
            {
                result.Vertices.Add(modifier(vertie, i));
            }
            return result.ToPathie();
        }

        public Pathie Apply(IPathieModifier modifier) => modifier.Modify(this);

        public override string ToString()
        {
            return $"V[{Vertices.Length}]";
        }

        public string Dump()
        {
            var vertices = string.Join("\n", Vertices.Select(v => v.ToString()));
            return $"V[{Vertices.Length}]\n{vertices}";
        }
        
        public static Pathie Empty() => new Pathie();

        public static PathieBuilder Builder() => new PathieBuilder();
        public static PathieBuilder Builder(Pathie pathie) => new PathieBuilder(pathie);

        public static PathieBuilder Builder(IEnumerable<Vertie> vertices)
        {
            var builder = new PathieBuilder();
            builder.Vertices.AddRange(vertices);
            return builder;
        }
    }
}