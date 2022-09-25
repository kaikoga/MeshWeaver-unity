using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models
{
    public class PathieBuilder
    {
        public readonly List<Vertie> Vertices;
        readonly bool _isLoop;
        readonly bool _smoothJoin;

        PathieBuilder(List<Vertie> vertices, bool isLoop, bool smoothJoin)
        {
            Vertices = vertices;
            _isLoop = isLoop;
            _smoothJoin = smoothJoin;
        }

        public PathieBuilder(bool isLoop, bool smoothJoin = false) : this(new List<Vertie>(), isLoop, smoothJoin) { }
        public PathieBuilder(Pathie pathie, bool isLoop, bool smoothJoin = false) : this(pathie.Vertices.ToList(), isLoop, smoothJoin) { }

        public PathieBuilder Concat(Pathie other, Matrix4x4 matrix4x4)
        {
            if (other.Vertices.Count > 0)
            {
                var vertices = matrix4x4 != Matrix4x4.identity ? other.Vertices.Select(v => v.MultiplyPoint(matrix4x4)) : other.Vertices;
                if (_smoothJoin && Vertices.Count > 0)
                {
                    var peekLast = Vertices[Vertices.Count - 1];
                    var takeFirst = vertices.First();
                    if (peekLast != takeFirst) Vertices.Add(takeFirst);

                    Vertices.AddRange(vertices.Skip(1));
                }
                else
                {
                    Vertices.AddRange(vertices);
                }
            }
            return this;
        }

        public Pathie ToPathie() => new Pathie(Vertices, _isLoop);
    }
}