using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public class Meshie
    {
        readonly Vertie[] _vertices;
        readonly int[] _indices;

        public IReadOnlyCollection<Vertie> Vertices => _vertices;
        public IReadOnlyCollection<int> Indices => _indices;

        Meshie(Vertie[] vertices, int[] indices)
        {
            _vertices = vertices;
            _indices = indices;
        }

        Meshie() : this(Array.Empty<Vertie>(), Array.Empty<int>()) { }

        public Meshie(IEnumerable<Vertie> vertices, IEnumerable<int> indices) : this(vertices.ToArray(), indices.ToArray()) { }

        public void ExportToMesh(Mesh mesh)
        {
            mesh.subMeshCount = 1;
            mesh.SetVertices(_vertices.Select(v => v.Vertex).ToArray());
            mesh.SetUVs(0, _vertices.Select(v => v.Uv).ToArray());
            mesh.SetTriangles(_indices.ToArray(), 0);
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
        }

        public Meshie Apply(IMeshieModifier modifier) => modifier.Modify(this);

        public override string ToString()
        {
            return $"V[{_vertices.Length}] I[{_indices.Length}]";
        }

        public string Dump()
        {
            var vertices = string.Join("\n", _vertices.Select(v => v.ToString()));
            var indices = string.Join(",", _indices.Select(v => v.ToString()));
            return $"V[{_vertices.Length}]\n{vertices}\nI[{_indices.Length}]\n{indices}";
        }

        public static Meshie Empty() => new Meshie();

        public static MeshieBuilder Builder() => new MeshieBuilder();
        public static MeshieBuilder Builder(Meshie meshie) => new MeshieBuilder(meshie);

        public static MeshieBuilder Builder(IEnumerable<Vertie> vertices, IEnumerable<int> indices, bool validateIndices = false)
        {
            var builder = new MeshieBuilder();
            builder.Vertices.AddRange(vertices);
            if (validateIndices)
            {
                builder.AddTriangleIndices(indices);
            }
            else
            {
                builder.Indices.AddRange(indices);
            }
            return builder;
        }
    }
}