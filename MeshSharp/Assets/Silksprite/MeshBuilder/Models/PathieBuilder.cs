using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public class PathieBuilder
    {
        public readonly List<Vertie> Vertices;

        PathieBuilder(List<Vertie> vertices)
        {
            Vertices = vertices;
        }

        public PathieBuilder() : this(new List<Vertie>()) { }
        public PathieBuilder(Pathie pathie) : this(pathie.Vertices.ToList()) { }

        public PathieBuilder Concat(Pathie other, Matrix4x4 matrix4x4)
        {
            Vertices.AddRange(other.Vertices.Select(v => v.MultiplyPoint(matrix4x4)));
            return this;
        }

        public Pathie ToPathie() => new Pathie(Vertices);
    }
}