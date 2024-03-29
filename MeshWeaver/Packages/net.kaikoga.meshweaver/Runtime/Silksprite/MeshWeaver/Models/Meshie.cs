using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models
{
    public class Meshie : IDump
    {
        readonly Vertie[] _vertices;
        readonly Gon[] _gons;

        // TODO: We can expose implementation when we have ImmutableArray
        public IReadOnlyList<Vertie> Vertices => _vertices;
        public IReadOnlyList<Gon> Gons => _gons;

        MeshExporter _exported;
        MeshExporter Exported => _exported ?? (_exported = new MeshExporter(_vertices, _gons));

        public Material[] Materials => Exported.Materials;

        Meshie(Vertie[] vertices, Gon[] gons)
        {
            _vertices = vertices;
            _gons = gons;
        }

        Meshie() : this(Array.Empty<Vertie>(), Array.Empty<Gon>()) { }

        public Meshie(IEnumerable<Vertie> vertices, IEnumerable<Gon> gons) : this(vertices.ToArray(), gons.ToArray()) { }

        public void ExportToMesh(Mesh mesh, MeshExportSettings settings)
        {
            Exported.WriteToMesh(mesh, settings);
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

        public static MeshieBuilder Builder(IEnumerable<Vertie> vertices, IEnumerable<int> indices, Material material, bool validateIndices = false)
        {
            var builder = new MeshieBuilder();
            builder.Vertices.AddRange(vertices);
            if (validateIndices)
            {
                builder.AddTriangles(indices, material);
            }
            else
            {
                builder.Gons.AddRange(indices.EachTrio((a, b, c) => new Gon(new []{ a, b, c }, material)));
            }
            return builder;
        }
    }
}