using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models.Extensions;
using Silksprite.MeshBuilder.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    public class Meshie
    {
        readonly Vertie[] _vertices;
        readonly Gon[] _gons;

        public IReadOnlyCollection<Vertie> Vertices => _vertices;
        public IReadOnlyCollection<Gon> Gons => _gons;

        Meshie(Vertie[] vertices, Gon[] gons)
        {
            _vertices = vertices;
            _gons = gons;
        }

        Meshie() : this(Array.Empty<Vertie>(), Array.Empty<Gon>()) { }

        public Meshie(IEnumerable<Vertie> vertices, IEnumerable<Gon> gons) : this(vertices.ToArray(), gons.ToArray()) { }

        public void ExportToMesh(Mesh mesh)
        {
            mesh.subMeshCount = 1;
            mesh.SetVertices(_vertices.Select(v => v.Vertex).ToArray());
            mesh.SetUVs(0, _vertices.Select(v => v.Uv).ToArray());
            mesh.SetTriangles(_gons.SelectMany(gon => gon.Indices).ToArray(), 0);
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
        }

        public Meshie Apply(IMeshieModifier modifier) => modifier.Modify(this);

        public override string ToString()
        {
            return $"V[{_vertices.Length}] T[{_gons.Length}]";
        }

        public string Dump()
        {
            var vertices = string.Join("\n", _vertices.Select(v => v.ToString()));
            var gons = string.Join("\n", _gons.Select(v => v.ToString()));
            return $"V[{_vertices.Length}]\n{vertices}\nT[{_gons.Length}]\n{gons}";
        }

        public static Meshie Empty() => new Meshie();

        public static MeshieBuilder Builder() => new MeshieBuilder();
        public static MeshieBuilder Builder(Meshie meshie) => new MeshieBuilder(meshie);

        public static MeshieBuilder Builder(IEnumerable<Vertie> vertices, IEnumerable<Gon> gons, bool validateIndices = false)
        {
            var builder = new MeshieBuilder();
            builder.Vertices.AddRange(vertices);
            if (validateIndices)
            {
                builder.AddTriangles(gons);
            }
            else
            {
                builder.Gons.AddRange(gons);
            }
            return builder;
        }

        public static MeshieBuilder Builder(IEnumerable<Vertie> vertices, IEnumerable<int> indices, int materialIndex, bool validateIndices = false)
        {
            var builder = new MeshieBuilder();
            builder.Vertices.AddRange(vertices);
            if (validateIndices)
            {
                builder.AddTriangles(indices, 0);
            }
            else
            {
                builder.Gons.AddRange(indices.EachTrio((a, b, c) => new Gon(new []{ a, b, c }, materialIndex)));
            }
            return builder;
        }
    }
}