using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;

namespace Silksprite.MeshWeaver.Models
{
    public class Pathie
    {
        public IReadOnlyCollection<Vertie> Vertices => _vertices;

        readonly Vertie[] _vertices;

        public Pathie Active => new Pathie(ActiveVertices());

        IEnumerable<Vertie> ActiveVertices()
        {
            return _vertices.Where((v, index) => index == 0 || index == _vertices.Length - 1 || !v.Culled);
        }

        public Vertie First => _vertices.Length > 0 ? _vertices[0] : default;
        public Vertie Last => _vertices.Length > 0 ? _vertices[_vertices.Length - 1] : default;
        
        public Vertie Diff => Last / First;


        Pathie(Vertie[] vertices)
        {
            _vertices = vertices;
        }

        Pathie() : this(Array.Empty<Vertie>()) { }

        public Pathie(Vertie soleVertex) : this(new List<Vertie> { soleVertex }) { }

        public Pathie(IEnumerable<Vertie> vertices) : this(vertices.ToArray()) { }

        public Pathie Modify(Func<Vertie, int, Vertie> modifier)
        {
            var result = new PathieBuilder();
            foreach (var (vertie, i) in _vertices.Select((vertie, i) => (vertie, i)))
            {
                result.Vertices.Add(modifier(vertie, i));
            }
            return result.ToPathie();
        }

        public Pathie Apply(IPathieModifier modifier) => modifier.Modify(this);

        public override string ToString()
        {
            return $"V[{_vertices.Length}]";
        }

        public string Dump()
        {
            var vertices = string.Join("\n", _vertices.Select(v => v.ToString()));
            return $"V[{_vertices.Length}]\n{vertices}";
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