using System.Linq;
using Silksprite.MeshBuilder.Models.Base;

namespace Silksprite.MeshBuilder.Models.Paths.Modifiers
{
    public class PathRepeat : PathModifier
    {
        readonly int _count;
        readonly bool _aggregate;

        public PathRepeat(int count, bool aggregate)
        {
            _count = count;
            _aggregate = aggregate;
        }

        public override Pathie Modify(Pathie pathie)
        {
            if (pathie.Vertices.Count <= 1) return pathie;
            if (_count <= 1) return pathie;

            if (_aggregate)
            {
                var diff = pathie.Vertices.Last() - pathie.Vertices.First();
                var vertices = pathie.Vertices.Take(1)
                    .Concat(Enumerable.Range(0, _count)
                        .Select(i =>
                        {
                            var iDiff = diff * i;
                            return pathie.Vertices.Skip(1).Select(v => v + iDiff);
                        })
                        .SelectMany(v => v));
                return new Pathie(vertices);
            }
            else
            {
                var diff = pathie.Vertices.Last().Vertex - pathie.Vertices.First().Vertex;
                var vertices = Enumerable.Range(0, _count)
                    .Select(i =>
                    {
                        var iDiff = diff * i;
                        return pathie.Vertices.Select(v => v + iDiff);
                    })
                    .SelectMany(v => v);
                return new Pathie(vertices);
            }
        }
    }
}