using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public class Meshie
    {
        public readonly List<Vertie> Vertices = new List<Vertie>();
        public readonly List<int> Indices = new List<int>();

        public Meshie() { }

        public Meshie(IEnumerable<Vertie> vertices, IEnumerable<int> indices)
        {
            Vertices.AddRange(vertices);
            Indices.AddRange(indices);
        }

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

        public void Concat(Meshie other, Matrix4x4 matrix4x4)
        {
            var offset = Vertices.Count;
            Vertices.AddRange(other.Vertices.Select(v => v.MultiplyPoint(matrix4x4)));
            Indices.AddRange(other.Indices.Select(i => i + offset));
        }

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
    }
}