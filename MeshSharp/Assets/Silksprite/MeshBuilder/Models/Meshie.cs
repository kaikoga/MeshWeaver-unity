using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public class Meshie
    {
        public readonly List<Vector3> Vertices = new List<Vector3>();
        public readonly List<int> Indices = new List<int>();

        public void ExportToMesh(Mesh mesh)
        {
            mesh.subMeshCount = 1;
            mesh.SetVertices(Vertices.ToArray());
            mesh.SetTriangles(Indices.ToArray(), 0);
        }

        public void Concat(Meshie other)
        {
            var offset = Vertices.Count;
            Vertices.AddRange(other.Vertices);
            Indices.AddRange(other.Indices.Select(i => i + offset));
        }
    }
}