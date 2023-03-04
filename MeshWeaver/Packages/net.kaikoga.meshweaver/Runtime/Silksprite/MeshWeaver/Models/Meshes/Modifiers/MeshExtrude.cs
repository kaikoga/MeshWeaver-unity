using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Meshes.Modifiers
{
    public class MeshExtrude : IMeshieModifier
    {
        readonly bool _fillBody;
        readonly bool _fillBottom;
        readonly bool _fillTop;
        readonly bool _reverseLids;
        readonly bool _isSmooth;
        readonly bool _isFromCenter;
        readonly Matrix4x4 _translation;

        public MeshExtrude(bool fillBody, bool fillBottom, bool fillTop, bool reverseLids, bool isSmooth, bool isFromCenter, Matrix4x4 translation)
        {
            _fillBody = fillBody;
            _fillBottom = fillBottom;
            _fillTop = fillTop;
            _reverseLids = reverseLids;
            _isSmooth = isSmooth;
            _isFromCenter = isFromCenter;
            _translation = translation;
        }

        public Meshie Modify(Meshie meshie)
        {
            var builder = Meshie.Builder();
            var edges = new Dictionary<(int a, int b), int>();
            var clonedIndices = new Dictionary<int, int>();

            void AddEdge(int a, int b)
            {
                edges[(a, b)] = (edges.TryGetValue((a, b), out var i) ? i : 0) + 1;
                edges[(b, a)] = (edges.TryGetValue((b, a), out var i2) ? i2 : 0) - 1;
            }

            if (_fillBottom)
            {
                builder.Concat(_reverseLids ? meshie : meshie.Apply(MeshReverse.BackOnly), Matrix4x4.identity, 0);
            }
            else if (_fillBody)
            {
                builder.Vertices.AddRange(meshie.Vertices);
            }

            var translation = _translation;
            if (_isFromCenter && meshie.Vertices.Count > 0)
            {
                var bounds = new Bounds(meshie.Vertices.First().Vertex, Vector3.zero);
                foreach (var v in meshie.Vertices)
                {
                    bounds.Encapsulate(v.Vertex);
                }
                translation = Matrix4x4.Translate(bounds.center) * _translation * Matrix4x4.Translate(-bounds.center);
            }
            if (_fillTop)
            {
                builder.Concat(_reverseLids ? meshie.Apply(MeshReverse.BackOnly) : meshie, translation, 0);
            }
            else if (_fillBody)
            {
                builder.Vertices.AddRange(meshie.Vertices.Select(v => v.MultiplyPoint(translation)));
            }
            if (_fillBody)
            {
                foreach (var gon in meshie.Gons)
                {
                    AddEdge(gon[0], gon[1]);
                    AddEdge(gon[1], gon[2]);
                    AddEdge(gon[2], gon[0]);
                }

                var verticesCount = meshie.Vertices.Count;
                var verticesOffset = builder.Vertices.Count;

                foreach (var kv in edges)
                {
                    if (kv.Value <= 0) continue;
                    var (a, b) = kv.Key;
                    var c = b + verticesCount;
                    var d = a + verticesCount;
                    if (!_isSmooth)
                    {
                        int CloneVertex(int v)
                        {
                            if (clonedIndices.TryGetValue(v, out var x)) return x;
                            x = verticesOffset++;
                            builder.Vertices.Add(builder.Vertices[v]);
                            clonedIndices.Add(v, x);
                            return x;
                        }

                        a = CloneVertex(a);
                        b = CloneVertex(b);
                        c = CloneVertex(c);
                        d = CloneVertex(d);
                    }
                    builder.AddTriangles(new[] { a, b, c, a, c, d }, null);
                }
            }
            return builder.ToMeshie();
        }
    }
}