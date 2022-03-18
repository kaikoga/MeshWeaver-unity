using System;
using System.IO;
using System.Linq;
using Silksprite.MeshBuilder.Models;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers
{
    [CustomEditor(typeof(MeshBehaviourExporter))]
    public class MeshBehaviourExporterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var meshBehaviourExporter = (MeshBehaviourExporter)target;
            var meshBehaviour = meshBehaviourExporter.GetComponent<CustomMeshBehaviour>();
            if (GUILayout.Button("Create Mesh Asset"))
            {
                var projectFilePath = EditorUtility.SaveFilePanelInProject("Export Mesh Asset",  meshBehaviourExporter.gameObject.name, "mesh", "");
                ExportMeshAsset(projectFilePath, meshBehaviourExporter, meshBehaviour);
            }

            if (GUILayout.Button("Update Mesh Asset"))
            {
                if (!AssetDatabase.IsMainAsset(meshBehaviourExporter.outputMesh))
                {
                    throw new InvalidOperationException();
                }
                var projectFilePath = AssetDatabase.GetAssetPath(meshBehaviourExporter.outputMesh);
                ExportMeshAsset(projectFilePath, meshBehaviourExporter, meshBehaviour);
            }

            if (GUILayout.Button("Create Mesh Prefab"))
            {
                var projectFilePath = EditorUtility.SaveFilePanelInProject("Export Mesh Prefab",  meshBehaviourExporter.gameObject.name, "prefab", "");
                ExportMeshPrefab(projectFilePath, meshBehaviourExporter);
            }

            if (GUILayout.Button("Update Mesh Prefab"))
            {
                if (!AssetDatabase.IsMainAsset(meshBehaviourExporter.outputPrefab))
                {
                    throw new InvalidOperationException();
                }
                var projectFilePath = AssetDatabase.GetAssetPath(meshBehaviourExporter.outputPrefab);
                ExportMeshPrefab(projectFilePath, meshBehaviourExporter);
            }
        }

        static void ExportMeshAsset(string projectFilePath, MeshBehaviourExporter meshBehaviourExporter, CustomMeshBehaviour meshBehaviour)
        {
            if (string.IsNullOrEmpty(projectFilePath))
            {
                throw new OperationCanceledException();
            }

            var meshes = AssetDatabase.LoadAllAssetsAtPath(projectFilePath).OfType<Mesh>().ToArray();
            var mesh0 = meshes.FirstOrDefault(AssetDatabase.IsMainAsset);
            var subAssets = meshes.Where(AssetDatabase.IsSubAsset).ToArray();
            var mesh1 = subAssets.FirstOrDefault(mesh => mesh.name.EndsWith("_LOD1"));
            var mesh2 = subAssets.FirstOrDefault(mesh => mesh.name.EndsWith("_LOD2"));
            var mesh9 = subAssets.FirstOrDefault(mesh => mesh.name.EndsWith("_Collider"));
            foreach (var subAsset in subAssets)
            {
                AssetDatabase.RemoveObjectFromAsset(subAsset);
            }

            if (!mesh0)
            {
                mesh0 = new Mesh();
                AssetDatabase.CreateAsset(mesh0, projectFilePath);
            }

            if (!mesh1)
            {
                mesh1 = new Mesh();
            }
            if (!mesh2)
            {
                mesh2 = new Mesh();
            }
            if (!mesh9)
            {
                mesh9 = new Mesh();
            }

            AssetDatabase.AddObjectToAsset(mesh1, projectFilePath);
            AssetDatabase.AddObjectToAsset(mesh2, projectFilePath);
            AssetDatabase.AddObjectToAsset(mesh9, projectFilePath);

            var baseName = Path.GetFileNameWithoutExtension(projectFilePath);
            meshBehaviour.ExportMesh(LodMaskLayer.LOD0, mesh0);
            mesh0.name = baseName;
            
            meshBehaviour.ExportMesh(LodMaskLayer.LOD1, mesh1);
            mesh1.name = mesh0.name + "_LOD1";

            meshBehaviour.ExportMesh(LodMaskLayer.LOD2, mesh2);
            mesh2.name = mesh0.name + "_LOD2";

            meshBehaviour.ExportMesh(LodMaskLayer.Collider, mesh9);
            mesh9.name = mesh0.name + "_Collider";

            meshBehaviourExporter.outputMesh = mesh0; 

            AssetDatabase.SaveAssets();
        }

        static void ExportMeshPrefab(string projectFilePath, MeshBehaviourExporter meshBehaviourExporter)
        {
            if (string.IsNullOrEmpty(projectFilePath))
            {
                throw new OperationCanceledException();
            }

            var meshes = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(meshBehaviourExporter.outputMesh)).OfType<Mesh>().ToArray();
            var mesh0 = meshes.FirstOrDefault(AssetDatabase.IsMainAsset);
            var subAssets = meshes.Where(AssetDatabase.IsSubAsset).ToArray();
            var mesh1 = subAssets.FirstOrDefault(mesh => mesh.name.EndsWith("_LOD1"));
            var mesh2 = subAssets.FirstOrDefault(mesh => mesh.name.EndsWith("_LOD2"));
            var mesh9 = subAssets.FirstOrDefault(mesh => mesh.name.EndsWith("_Collider"));

            var material = meshBehaviourExporter.GetComponent<MeshRenderer>().sharedMaterial;

            var baseName = Path.GetFileNameWithoutExtension(projectFilePath);
            var prefab = new GameObject(baseName);

            var gameObject0 = new GameObject(baseName + "_LOD0");
            gameObject0.transform.SetParent(prefab.transform, false);
            var meshFilter0 = gameObject0.AddComponent<MeshFilter>();
            meshFilter0.sharedMesh = mesh0;
            var meshRenderer0 = gameObject0.AddComponent<MeshRenderer>();
            meshRenderer0.sharedMaterial = material;

            var gameObject1 = new GameObject(baseName + "_LOD1");
            gameObject1.transform.SetParent(prefab.transform, false);
            var meshFilter1 = gameObject1.AddComponent<MeshFilter>();
            meshFilter1.sharedMesh = mesh1;
            var meshRenderer1 = gameObject1.AddComponent<MeshRenderer>();
            meshRenderer1.sharedMaterial = material;

            var gameObject2 = new GameObject(baseName + "_LOD2");
            gameObject2.transform.SetParent(prefab.transform, false);
            var meshFilter2 = gameObject2.AddComponent<MeshFilter>();
            meshFilter2.sharedMesh = mesh2;
            var meshRenderer2 = gameObject2.AddComponent<MeshRenderer>();
            meshRenderer2.sharedMaterial = material;

            var gameObject9 = new GameObject(baseName + "_Collider");
            gameObject9.transform.SetParent(prefab.transform, false);
            var collider = gameObject9.AddComponent<MeshCollider>();
            collider.sharedMesh = mesh9;

            var lodGroup = prefab.AddComponent<LODGroup>();
            lodGroup.SetLODs(new []
            {
                new LOD
                {
                    screenRelativeTransitionHeight = 0.6f,
                    fadeTransitionWidth = 0,
                    renderers = new Renderer[]
                    {
                        meshRenderer0
                    }
                },
                new LOD
                {
                    screenRelativeTransitionHeight = 0.3f,
                    fadeTransitionWidth = 0,
                    renderers = new Renderer[]
                    {
                        meshRenderer1
                    }
                },
                new LOD
                {
                    screenRelativeTransitionHeight = 0.1f,
                    fadeTransitionWidth = 0,
                    renderers = new Renderer[]
                    {
                        meshRenderer2
                    }
                }
            });

            PrefabUtility.SaveAsPrefabAsset(prefab, projectFilePath);
            DestroyImmediate(prefab);
        }
    }
}