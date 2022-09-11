using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models
{
    public class PathieBuilder
    {
        public readonly List<Vertie> Vertices;
        public readonly bool IsLoop;

        PathieBuilder(List<Vertie> vertices, bool isLoop)
        {
            Vertices = vertices;
            IsLoop = isLoop;
        }

        public PathieBuilder(bool isLoop) : this(new List<Vertie>(), isLoop) { }
        public PathieBuilder(Pathie pathie, bool isLoop) : this(pathie.Vertices.ToList(), isLoop) { }

        public PathieBuilder Concat(Pathie other, Matrix4x4 matrix4x4)
        {
            var vertices = matrix4x4 != Matrix4x4.identity ? other.Vertices.Select(v => v.MultiplyPoint(matrix4x4)) : other.Vertices;
            Vertices.AddRange(vertices);
            return this;
        }

        public Pathie ToPathie() => new Pathie(Vertices, IsLoop);
    }
}