using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Meshes.Modifiers
{
    public class MeshCutout : IMeshieModifier
    {
        readonly Func<Vector3, bool> _predicate;
        readonly bool _inside;
        readonly int _numVertex;

        public MeshCutout(Func<Vector3, bool> predicate, bool inside, int numVertex)
        {
            _predicate = predicate;
            _inside = inside;
            _numVertex = numVertex;
        }

        public Meshie Modify(Meshie meshie)
        {
            var vertices = meshie.Vertices;

            bool Predicate(Vector3 point) => _inside == _predicate(point);

            IEnumerable<Gon> gons;
            if (_numVertex == 0)
            {
                gons = meshie.Gons.Where(gon =>
                {
                    var centerPoint = gon.Indices.Select(i => vertices[i].Vertex).Aggregate((a, b) => a + b) / gon.Indices.Count;
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

            return Meshie.Builder(vertices, gons).ToMeshie(true);
        }
    }
}