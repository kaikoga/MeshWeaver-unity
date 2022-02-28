using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public class Meshie
    {
        public readonly List<Vertie> Vertices = new List<Vertie>();
        public readonly List<int> Indices = new List<int>();

        public void ExportToMesh(Mesh mesh)
        {
            mesh.subMeshCount = 1;
            mesh.SetVertices(Vertices.Select(v => v.Vertex).ToArray());
            mesh.SetUVs(0, Vertices.Select(v => v.Uv).ToArray());
            mesh.SetTriangles(Indices.ToArray(), 0);
        }

        public void Concat(Meshie other, Matrix4x4 matrix4x4)
        {
            var offset = Vertices.Count;
            Vertices.AddRange(other.Vertices.Select(v => v.MultiplyPoint(matrix4x4)));
            Indices.AddRange(other.Indices.Select(i => i + offset));
        }
    }
}