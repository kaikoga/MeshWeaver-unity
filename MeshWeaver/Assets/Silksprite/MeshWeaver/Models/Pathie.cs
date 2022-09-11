using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;

namespace Silksprite.MeshWeaver.Models
{
    public class Pathie
    {
        public IReadOnlyList<Vertie> Vertices => _vertices;
        public readonly bool isLoop;

        // TODO: We can expose implementation when we have ImmutableArray
        readonly Vertie[] _vertices;

        public Vertie First => _vertices.Length > 0 ? _vertices[0] : Vertie.Identity;
        public Vertie Last
        {
            get
            {
                if (isLoop) return First;
                return _vertices.Length > 0 ? _vertices[_vertices.Length - 1] : Vertie.Identity;
            }
        }

        public Vertie Diff
        {
            get
            {
                if (isLoop) return Vertie.Identity;
                return _vertices.Length > 0 ? Last / First : Vertie.Identity;
            }
        }

        Pathie(Vertie[] vertices, bool isLoop)
        {
            _vertices = vertices;
            this.isLoop = isLoop;
        }

        Pathie() : this(Array.Empty<Vertie>(), true) { }

        public Pathie(Vertie soleVertex) : this(new List<Vertie> { soleVertex }, true) { }

        public Pathie(IEnumerable<Vertie> vertices, bool isLoop) : this(vertices.ToArray(), isLoop) { }

        public Pathie Modify(Func<Vertie, int, Vertie> modifier)
        {
            var result = new PathieBuilder(isLoop);
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
            return $"V[{_vertices.Length}]{(isLoop ? "L" : "")}\n{vertices}";
        }
        
        public static Pathie Empty() => new Pathie();

        public static PathieBuilder Builder(bool isLoop) => new PathieBuilder(isLoop);
        public static PathieBuilder Builder(Pathie pathie, bool isLoop) => new PathieBuilder(pathie, isLoop);

        public static PathieBuilder Builder(IEnumerable<Vertie> vertices, bool isLoop)
        {
            var builder = new PathieBuilder(isLoop);
            builder.Vertices.AddRange(vertices);
            return builder;
        }
    }
}