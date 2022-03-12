using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models.Base;

namespace Silksprite.MeshBuilder.Models.Paths.Modifiers
{
    public class PathieSubdivision : PathieModifier
    {
        readonly int _count;

        public PathieSubdivision(int count)
        {
            _count = count;
        }

        public override Pathie Modify(Pathie pathie)
        {
            if (pathie.Vertices.Count <= 1) return pathie;
            if (_count <= 1) return pathie;

            IEnumerable<Vertie> SubdivideNextEdge(Vertie a, Vertie b)
            {
                for (var i = 1; i < _count; i++)
                {
                    var f = (float)i / _count;
                    yield return a * (1 - f) + b * f;
                }
                yield return b;
            }

            var vertices = pathie.Vertices.Take(1)
                .Concat(pathie.Pairwise(SubdivideNextEdge).SelectMany(v => v));
            return new Pathie(vertices);
        }
    }
}