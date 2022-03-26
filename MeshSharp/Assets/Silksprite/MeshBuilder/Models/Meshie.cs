using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public class Meshie
    {
        public readonly List<Vertie> Vertices;
        public readonly List<int> Indices;

        Meshie(List<Vertie> vertices, List<int> indices)
        {
            Vertices = vertices;
            Indices = indices;
        }

        Meshie() : this(new List<Vertie>(), new List<int>()) { }

        public Meshie(IEnumerable<Vertie> vertices, IEnumerable<int> indices) : this(vertices.ToList(), indices.ToList()) { }

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
            return $"V[{Vertices.Count}] I[{Indices.Count}]";
        }

        public string Dump()
        {
            var vertices = string.Join("\n", Vertices.Select(v => v.ToString()));
            var indices = string.Join(",", Indices.Select(v => v.ToString()));
            return $"V[{Vertices.Count}]\n{vertices}\nI[{Indices.Count}]\n{indices}";
        }

        public static Meshie Empty() => new Meshie();
        public static MeshieBuilder Builder() => new MeshieBuilder();
    }
}