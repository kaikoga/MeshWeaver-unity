using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Meshes.Modifiers
{
    public class MeshCutoutUvBounds : IMeshieModifier
    {
        readonly Vector2 _min;
        readonly Vector2 _max;
        readonly int _uvChannel;
        readonly bool _inside;
        readonly int _numVertex;

        public MeshCutoutUvBounds(Vector2 min, Vector2 max, int uvChannel, bool inside, int numVertex)
        {
            _min = min;
            _max = max;
            _uvChannel = uvChannel;
            _inside = inside;
            _numVertex = numVertex;
        }

        public Meshie Modify(Meshie meshie)
        {
            var vertices = meshie.Vertices.ToArray();

            bool Predicate(Vector2 uv) => _inside ? new Rect(_min, _max).Contains(uv) : !new Rect(_min, _max).Contains(uv);

            IEnumerable<Gon> gons;
            if (_numVertex == 0)
            {
                gons = meshie.Gons.Where(gon =>
                {
                    var centerPoint = gon.Indices.Select(i => vertices[i].Uvs.ValueAt(_uvChannel)).Aggregate(Vector2.zero, (a, b) => a + b) / gon.Indices.Count;
                    return Predicate(centerPoint);
                });
            }
            else
            {
                gons = meshie.Gons.Where(gon =>
                {
                    var numPoints = gon.Indices.Select(i => vertices[i].Uvs.ValueAt(_uvChannel)).Count(Predicate);
                    return numPoints >= _numVertex;
                });
            }

            return Meshie.Builder(vertices, gons).ToMeshie(true);
        }
    }
}