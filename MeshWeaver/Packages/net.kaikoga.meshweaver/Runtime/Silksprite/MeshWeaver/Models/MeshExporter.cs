using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models
{
    public class MeshExporter
    {
        readonly Vertie[] _vertices;

        readonly int[][] _subMeshes;
        readonly Material[] _materials;

        public Material[] Materials => _materials.ToArray();

        public MeshExporter(Vertie[] vertices, Gon[] gons)
        {
            _vertices = vertices;

            var subMeshes = gons.GroupBy(gon => gon.material).ToArray(); 
            _subMeshes = subMeshes.Select(subMesh => subMesh.SelectMany(gon => gon.Indices).ToArray()).ToArray();
            _materials = subMeshes.Select(subMesh => subMesh.Key).ToArray();
            for (var i = 0; i < _materials.Length; i++)
            {
                if (_materials[i] == null) _materials[i] = _materials.FirstOrDefault(m => m);
            }
        }

        public void WriteToMesh(Mesh mesh, MeshExportSettings settings)
        {
            mesh.subMeshCount = _subMeshes.Length > 0 ? _subMeshes.Length : 1;
            mesh.SetVertices(_vertices.Select(v => v.Vertex).ToArray());
            mesh.SetUVs(0, _vertices.Select(v => v.Uv).ToArray());
            for (var subMeshIndex = 0; subMeshIndex < _subMeshes.Length; subMeshIndex++)
            {
                mesh.SetTriangles(_subMeshes[subMeshIndex], subMeshIndex);
            }
            mesh.RecalculateBounds();
            switch (settings.NormalGenerator)
            {
                case MeshExportSettings.NormalGeneratorKind.Default:
                    mesh.RecalculateNormals();
                    break;
                case MeshExportSettings.NormalGeneratorKind.Up:
                    mesh.SetNormals(Enumerable.Repeat(Vector3.up, mesh.vertexCount).ToArray());
                    break;
                case MeshExportSettings.NormalGeneratorKind.Down:
                    mesh.SetNormals(Enumerable.Repeat(Vector3.down, mesh.vertexCount).ToArray());
                    break;
                case MeshExportSettings.NormalGeneratorKind.Sphere:
                    ProjectSphereNormals(mesh);
                    break;
                case MeshExportSettings.NormalGeneratorKind.Cylinder:
                    ProjectCylinderNormals(mesh);
                    break;
                case MeshExportSettings.NormalGeneratorKind.Smooth:
                    RecalculateNormals(mesh);
                    break;
                case MeshExportSettings.NormalGeneratorKind.SmoothHigh:
                    RecalculateNormals(mesh, true);
                    break;
                case MeshExportSettings.NormalGeneratorKind.None:
                    break;
                default:
                    mesh.RecalculateNormals();
                    break;
            }
            mesh.RecalculateTangents();
            switch (settings.LightmapGenerator)
            {
                case MeshExportSettings.LightmapGeneratorKind.None:
                    break;
                default:
#if UNITY_EDITOR
                    if (mesh.vertexCount > 0)
                    {
                        UnityEditor.Unwrapping.GenerateSecondaryUVSet(mesh, new UnityEditor.UnwrapParam
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

        static void ProjectSphereNormals(Mesh mesh)
        {
            var vertices = mesh.vertices;
            var center = vertices.Aggregate(Vector3.zero, (a, b) => a + b) / vertices.Length;
            
            mesh.SetNormals(mesh.vertices.Select(v => (v - center).normalized).ToArray());
        }

        static void ProjectCylinderNormals(Mesh mesh)
        {
            var vertices = mesh.vertices;
            var center = vertices.Aggregate(Vector3.zero, (a, b) => a + b) / vertices.Length;
            
            mesh.SetNormals(mesh.vertices.Select(v => new Vector3(v.x - center.x, 0, v.z - center.z).normalized).ToArray());
        }

        static void RecalculateNormals(Mesh mesh, bool addAreaWeight = false)
        {
            var vertices = mesh.vertices;

            // normal index is the first identical normal
            Vector3 Quantize(Vector3 v)
            {
                const float q = 10000f;
                return new Vector3(Mathf.Round(v.x * q) / q, Mathf.Round(v.y * q) / q, Mathf.Round(v.z * q) / q);
            }

            var vertexValueToNormalIndex = new Dictionary<Vector3, int>();
            for (var i = mesh.vertices.Length - 1; i >= 0; i--)
            {
                vertexValueToNormalIndex[Quantize(mesh.vertices[i])] = i;
            }

            int VertexValueToNormalIndex(Vector3 v)
            {
                return vertexValueToNormalIndex[Quantize(v)];
            }

            // calculate the normals
            var vertNormals = new Vector3[vertices.Length];
            var triangles = mesh.triangles;
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
            
            mesh.SetNormals(vertices.Select(v => vertNormals[VertexValueToNormalIndex(v)].normalized).ToArray());
            // _mesh.SetNormals(vertNormals.Select(v => v.normalized).ToArray());
        }
    }
}