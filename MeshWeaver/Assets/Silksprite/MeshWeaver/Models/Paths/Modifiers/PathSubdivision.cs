using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Paths.Modifiers
{
    public class PathSubdivision : IPathieModifier
    {
        readonly int _maxCount;
        readonly float _maxLength;

        public PathSubdivision(int maxCount, float maxLength)
        {
            _maxCount = maxCount;
            _maxLength = maxLength;
        }

        public Pathie Modify(Pathie pathie)
        {
            if (pathie.Vertices.Count <= 1) return pathie;
            if (_maxCount <= 1) return pathie;

            IEnumerable<Vertie> SubdivideNextEdge(Vertie a, Vertie b)
            {
                var count = _maxLength <= 0 ? _maxCount : Mathf.Clamp(Mathf.CeilToInt((b.Vertex - a.Vertex).magnitude / _maxLength), 1, _maxCount);
                for (var i = 1; i < count; i++)
                {
                    var f = (float)i / count;
                    yield return a * (1 - f) + b * f;
                }
                yield return b;
            }

            var vertices = pathie.Vertices.Take(1)
                .Concat(pathie.Vertices.Pairwise(SubdivideNextEdge).SelectMany(v => v));
            return new Pathie(vertices, pathie.isLoop);
        }
    }
}