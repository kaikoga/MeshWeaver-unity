using System;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models
{
    public class MeshExporter
    {
        readonly Mesh _mesh;
        readonly MeshExportSettings _settings;

        readonly Vertie[] _vertices;
        readonly Gon[] _gons;

        public MeshExporter(Mesh mesh, MeshExportSettings settings, Vertie[] vertices, Gon[] gons)
        {
            _mesh = mesh;
            _settings = settings;
            _vertices = vertices;
            _gons = gons;
        }

        public void Export()
        {
            var subMeshes = _gons.GroupBy(gon => gon.MaterialIndex).OrderBy(group => group.Key).ToArray();
            _mesh.subMeshCount = Math.Max(subMeshes.Length, 1);
            _mesh.SetVertices(_vertices.Select(v => v.Vertex).ToArray());
            _mesh.SetUVs(0, _vertices.Select(v => v.Uv).ToArray());
            for (var i = 0; i < subMeshes.Length; i++)
            {
                _mesh.SetTriangles(subMeshes[i].SelectMany(gon => gon.Indices).ToArray(), i);
            }
            _mesh.RecalculateBounds();
            switch (_settings.NormalGenerator)
            {
                case MeshExportSettings.NormalGeneratorKind.Default:
                    _mesh.RecalculateNormals();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _mesh.RecalculateTangents();
        }
    }
}