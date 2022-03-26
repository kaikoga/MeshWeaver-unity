using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public class Meshie
    {
        public readonly Vertie[] Vertices;
        public readonly int[] Indices;

        Meshie(Vertie[] vertices, int[] indices)
        {
            Vertices = vertices;
            Indices = indices;
        }

        Meshie() : this(Array.Empty<Vertie>(), Array.Empty<int>()) { }

        public Meshie(IEnumerable<Vertie> vertices, IEnumerable<int> indices) : this(vertices.ToArray(), indices.ToArray()) { }

        public void ExportToMesh(Mesh mesh)
        {
            mesh.subMeshCount = 1;
            mesh.SetVertices(Vertices.Select(v => v.Vertex).ToArray());
            mesh.SetUVs(0, Vertices.Select(v => v.Uv).ToArray());
            mesh.SetTriangles(Indices.ToArray(), 0);
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
        }

        public Meshie Apply(IMeshieModifier modifier) => modifier.Modify(this);

        public override string ToString()
        {
            return $"V[{Vertices.Length}] I[{Indices.Length}]";
        }

        public string Dump()
        {
            var vertices = string.Join("\n", Vertices.Select(v => v.ToString()));
            var indices = string.Join(",", Indices.Select(v => v.ToString()));
            return $"V[{Vertices.Length}]\n{vertices}\nI[{Indices.Length}]\n{indices}";
        }

        public static Meshie Empty() => new Meshie();
        public static MeshieBuilder Builder() => new MeshieBuilder();
        public static MeshieBuilder Builder(Meshie meshie) => new MeshieBuilder(meshie);
    }
}