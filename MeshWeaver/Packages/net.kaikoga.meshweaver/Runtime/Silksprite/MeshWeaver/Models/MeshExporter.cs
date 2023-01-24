using System.Collections.Generic;
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

        // FIXME: also export materials array because submesh indexes are determined here
        public MeshExporter(Mesh mesh, MeshExportSettings settings, Vertie[] vertices, Gon[] gons)
        {
            _mesh = mesh;
            _settings = settings;
            _vertices = vertices;
            _gons = gons;
        }

        public void Export()
        {
            var defaultMaterial = _gons.Where(gon => gon.material).Select(gon => gon.material).FirstOrDefault();
            
            var subMeshes = _gons.GroupBy(gon => gon.material ? gon.material : defaultMaterial).ToArray();
            _mesh.subMeshCount = subMeshes.Length > 0 ? subMeshes.Length : 1;
            _mesh.SetVertices(_vertices.Select(v => v.Vertex).ToArray());
            _mesh.SetUVs(0, _vertices.Select(v => v.Uv).ToArray());
            foreach (var subMesh in subMeshes.Select((data, i) => (data, i)))
            {
                _mesh.SetTriangles(subMesh.data.SelectMany(gon => gon.Indices).ToArray(), subMesh.i);
            }
            _mesh.RecalculateBounds();
            switch (_settings.NormalGenerator)
            {
                case MeshExportSettings.NormalGeneratorKind.Default:
                    _mesh.RecalculateNormals();
                    break;
                case MeshExportSettings.NormalGeneratorKind.Up:
                    _mesh.SetNormals(Enumerable.Repeat(Vector3.up, _mesh.vertexCount).ToArray());
                    break;
                case MeshExportSettings.NormalGeneratorKind.Down:
                    _mesh.SetNormals(Enumerable.Repeat(Vector3.down, _mesh.vertexCount).ToArray());
                    break;
                case MeshExportSettings.NormalGeneratorKind.Sphere:
                    ProjectSphereNormals();
                    break;
                case MeshExportSettings.NormalGeneratorKind.Cylinder:
                    ProjectCylinderNormals();
                    break;
                case MeshExportSettings.NormalGeneratorKind.Smooth:
                    RecalculateNormals();
                    break;
                case MeshExportSettings.NormalGeneratorKind.SmoothHigh:
                    RecalculateNormals(true);
                    break;
                case MeshExportSettings.NormalGeneratorKind.None:
                    break;
                default:
                    _mesh.RecalculateNormals();
                    break;
            }
            _mesh.RecalculateTangents();
            switch (_settings.LightmapGenerator)
            {
                case MeshExportSettings.LightmapGeneratorKind.None:
                    break;
                default:
#if UNITY_EDITOR
                    if (_mesh.vertexCount > 0)
                    {
                        UnityEditor.Unwrapping.GenerateSecondaryUVSet(_mesh, new UnityEditor.UnwrapParam
                        {
                            angleError = 0.08f,
                            areaError = 0.15f,
                            hardAngle = 88f,
                            packMargin = 0.03125f // 1/32
                        });
                    }
#endif
                    break;
            }
        }

        void ProjectSphereNormals()
        {
            var vertices = _mesh.vertices;
            var center = vertices.Aggregate(Vector3.zero, (a, b) => a + b) / vertices.Length;
            
            _mesh.SetNormals(_mesh.vertices.Select(v => (v - center).normalized).ToArray());
        }

        void ProjectCylinderNormals()
        {
            var vertices = _mesh.vertices;
            var center = vertices.Aggregate(Vector3.zero, (a, b) => a + b) / vertices.Length;
            
            _mesh.SetNormals(_mesh.vertices.Select(v => new Vector3(v.x - center.x, 0, v.z - center.z).normalized).ToArray());
        }

        void RecalculateNormals(bool addAreaWeight = false)
        {
            var vertices = _mesh.vertices;

            // normal index is the first identical normal
            Vector3 Quantize(Vector3 v)
            {
                const float q = 10000f;
                return new Vector3(Mathf.Round(v.x * q) / q, Mathf.Round(v.y * q) / q, Mathf.Round(v.z * q) / q);
            }

            var vertexValueToNormalIndex = new Dictionary<Vector3, int>();
            for (var i = _mesh.vertices.Length - 1; i >= 0; i--)
            {
                vertexValueToNormalIndex[Quantize(_mesh.vertices[i])] = i;
            }

            int VertexValueToNormalIndex(Vector3 v)
            {
                return vertexValueToNormalIndex[Quantize(v)];
            }

            // calculate the normals
            var vertNormals = new Vector3[vertices.Length];
            var triangles = _mesh.triangles;
            for (var face = 0; face < triangles.Length; face += 3)
            {
                var a = vertices[triangles[face]];
                var b = vertices[triangles[face + 1]];
                var c = vertices[triangles[face + 2]];
                var faceNormal = Vector3.Cross( b - a, c - a);
                if (addAreaWeight)
                {
                    var faceArea = Mathf.Abs((c - b).magnitude * Mathf.Sin(Vector3.Angle(b - a, c - a)) / 2f);
                    faceNormal *= Mathf.Sqrt(faceArea);
                }
                
                vertNormals[VertexValueToNormalIndex(a)] += faceNormal;
                vertNormals[VertexValueToNormalIndex(b)] += faceNormal;
                vertNormals[VertexValueToNormalIndex(c)] += faceNormal;
                // vertNormals[triangles[face]] += faceNormal;
                // vertNormals[triangles[face + 1]] += faceNormal;
                // vertNormals[triangles[face + 2]] += faceNormal;
            }
            
            _mesh.SetNormals(vertices.Select(v => vertNormals[VertexValueToNormalIndex(v)].normalized).ToArray());
            // _mesh.SetNormals(vertNormals.Select(v => v.normalized).ToArray());
        }
    }
}