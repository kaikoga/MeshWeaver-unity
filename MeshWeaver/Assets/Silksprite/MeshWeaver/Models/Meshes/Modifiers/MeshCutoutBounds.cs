using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Meshes.Modifiers
{
    public class MeshCutoutBounds : IMeshieModifier
    {
        readonly Bounds _bounds;
        readonly bool _inside;
        readonly int _numVertex;

        public MeshCutoutBounds(Bounds bounds, bool inside, int numVertex)
        {
            _bounds = bounds;
            _inside = inside;
            _numVertex = numVertex;
        }

        public Meshie Modify(Meshie meshie)
        {
            var vertices = meshie.Vertices.ToArray();

            var bounds = _bounds;
            bool Predicate(Vector3 point) => _inside ? bounds.Contains(point) : !bounds.Contains(point);

            IEnumerable<Gon> gons;
            if (_numVertex == 0)
            {
                gons = meshie.Gons.Where(gon =>
                {
                    var centerPoint = gon.Indices.Select(i => vertices[i].Vertex).Aggregate(Vector3.zero, (a, b) => a + b) / gon.Indices.Count;
                    return Predicate(centerPoint);
                });
            }
            else
            {
                gons = meshie.Gons.Where(gon =>
                {
                    var numPoints = gon.Indices.Select(i => vertices[i].Vertex).Count(Predicate);
                    return numPoints >= _numVertex;
                });
            }

            return Meshie.Builder(vertices, gons).ToMeshie();
        }
    }
}