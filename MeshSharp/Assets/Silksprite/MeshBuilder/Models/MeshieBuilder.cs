using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models.Extensions;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public class MeshieBuilder
    {
        public readonly List<Vertie> Vertices;
        public readonly List<Gon> Gons;

        MeshieBuilder(List<Vertie> vertices, List<Gon> gons)
        {
            Vertices = vertices;
            Gons = gons;
        }

        public MeshieBuilder() : this(new List<Vertie>(), new List<Gon>()) { }
        public MeshieBuilder(Meshie meshie) : this(meshie.Vertices.ToList(), meshie.Gons.ToList()) { }

        public MeshieBuilder Concat(Meshie meshie, Matrix4x4 matrix4x4, int uvIndex)
        {
            var offset = Vertices.Count;
            Vertices.AddRange(meshie.Vertices.Select(v => v.MultiplyPoint(matrix4x4).ShiftUvChannel(uvIndex)));
            Gons.AddRange(meshie.Gons.Select(gon => gon + offset));
            return this;
        }

        public MeshieBuilder AddTriangles(IEnumerable<int> indices, int materialIndex)
        {
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
                Gons.Add(new Gon(trio, materialIndex));
            }
            return this;
        }


        public MeshieBuilder AddTriangles(IEnumerable<Gon> gons)
        {
            foreach (var gon in gons)
            {
                if (Vertices[gon[0]].VertexEquals(Vertices[gon[1]]))
                {
                    continue;
                }
                if (Vertices[gon[1]].VertexEquals(Vertices[gon[2]]))
                {
                    continue;
                }
                if (Vertices[gon[0]].VertexEquals(Vertices[gon[2]]))
                {
                    continue;
                }
                Gons.Add(gon);
            }
            return this;
        }

        public Meshie ToMeshie() => new Meshie(Vertices, Gons);
    }
}