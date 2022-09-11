using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models
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
            IEnumerable<Vertie> vertices;
            if (matrix4x4 != Matrix4x4.identity)
            {
                var selector = uvIndex != 0 ? (Func<Vertie, Vertie>)(v => v.MultiplyPoint(matrix4x4).ShiftUvChannel(uvIndex)) : v => v.MultiplyPoint(matrix4x4);
                vertices = meshie.Vertices.Select(selector);
            }
            else
            {
                vertices = uvIndex != 0 ? meshie.Vertices.Select(v => v.ShiftUvChannel(uvIndex)) : meshie.Vertices;
            }

            Vertices.AddRange(vertices);
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

        public Meshie ToMeshie(bool cleanupStaleVertices = false)
        {
            if (!cleanupStaleVertices) return new Meshie(Vertices, Gons);

            var used = new bool[Vertices.Count];
            foreach (var i in Gons.SelectMany(gon => gon.Indices))
            {
                used[i] = true;
            }

            var indices = new int[Vertices.Count];
            var index = 0;
            for (var i = 0; i < indices.Length; i++)
            {
                indices[i] = used[i] ? index++ : -1;
            }

            return new Meshie(
                Vertices.Where((v, i) => used[i]),
                Gons.Select(gon => new Gon(gon.Indices.Select(i => indices[i]).ToArray(), gon.MaterialIndex)));
        }
    }
}