using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Extensions;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public class MeshieBuilder
    {
        public readonly List<Vertie> Vertices;
        public readonly List<int> Indices;

        MeshieBuilder(List<Vertie> vertices, List<int> indices)
        {
            Vertices = vertices;
            Indices = indices;
        }

        public MeshieBuilder() : this(new List<Vertie>(), new List<int>()) { }
        public MeshieBuilder(Meshie meshie) : this(meshie.Vertices.ToList(), meshie.Indices.ToList()) { }

        public MeshieBuilder Concat(Meshie meshie, Matrix4x4 matrix4x4, int uvIndex)
        {
            var offset = Vertices.Count;
            Vertices.AddRange(meshie.Vertices.Select(v => v.MultiplyPoint(matrix4x4).ShiftUvChannel(uvIndex)));
            Indices.AddRange(meshie.Indices.Select(i => i + offset));
            return this;
        }

        public MeshieBuilder AddTriangleIndices(IEnumerable<int> indices)
        {
            // Indices.AddRange(indices);
            // return this;
            foreach (var trio in indices.EachTrio((a, b, c) => new [] { a, b, c }))
            {
                if (Vertices[trio[0]].VertexEquals(Vertices[trio[1]]))
                {
                    continue;
                }
                if (Vertices[trio[1]].VertexEquals(Vertices[trio[2]]))
                {
                    continue;
                }
                if (Vertices[trio[0]].VertexEquals(Vertices[trio[2]]))
                {
                    continue;
                }
                Indices.AddRange(trio);
            }
            return this;
        }

        public Meshie ToMeshie() => new Meshie(Vertices, Indices);
    }
}