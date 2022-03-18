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

        static void RefreshMeshReferences(string projectFilePath, MeshBehaviourExporter meshBehaviourExporter, bool reconnect)
        {
            var meshes = AssetDatabase.LoadAllAssetsAtPath(projectFilePath).OfType<Mesh>().ToArray();
            meshBehaviourExporter.outputMesh = meshes.FirstOrDefault(AssetDatabase.IsMainAsset);
            
            var subAssets = meshes.Where(AssetDatabase.IsSubAsset).ToArray();
            meshBehaviourExporter.outputMeshLod1 = subAssets.FirstOrDefault(mesh => mesh.name.EndsWith("_LOD1"));
            meshBehaviourExporter.outputMeshLod2 = subAssets.FirstOrDefault(mesh => mesh.name.EndsWith("_LOD2"));
            meshBehaviourExporter.outputMeshForCollider = subAssets.FirstOrDefault(mesh => mesh.name.EndsWith("_Collider"));

            if (!reconnect) return;

            foreach (var subAsset in subAssets)
            {
                AssetDatabase.RemoveObjectFromAsset(subAsset);
            }

            if (!meshBehaviourExporter.outputMesh)
            {
                meshBehaviourExporter.outputMesh = new Mesh();
                AssetDatabase.CreateAsset(meshBehaviourExporter.outputMesh, projectFilePath);
            }

            if (!meshBehaviourExporter.outputMeshLod1)
            {
                meshBehaviourExporter.outputMeshLod1 = new Mesh();
            }
            if (!meshBehaviourExporter.outputMeshLod2)
            {
                meshBehaviourExporter.outputMeshLod2 = new Mesh();
            }
            if (!meshBehaviourExporter.outputMeshForCollider)
            {
                meshBehaviourExporter.outputMeshForCollider = new Mesh();
            }

            AssetDatabase.AddObjectToAsset(meshBehaviourExporter.outputMeshLod1, projectFilePath);
            AssetDatabase.AddObjectToAsset(meshBehaviourExporter.outputMeshLod2, projectFilePath);
            AssetDatabase.AddObjectToAsset(meshBehaviourExporter.outputMeshForCollider, projectFilePath);

            var baseName = Path.GetFileNameWithoutExtension(projectFilePath);
            meshBehaviourExporter.outputMesh.name = baseName;
            meshBehaviourExporter.outputMeshLod1.name = baseName + "_LOD1";
            meshBehaviourExporter.outputMeshLod2.name = baseName + "_LOD2";
            meshBehaviourExporter.outputMeshForCollider.name = baseName + "_Collider";
        }

        static void ExportMeshAsset(string projectFilePath, MeshBehaviourExporter meshBehaviourExporter, CustomMeshBehaviour meshBehaviour)
        {
            if (string.IsNullOrEmpty(projectFilePath))
            {
                throw new OperationCanceledException();
            }

            RefreshMeshReferences(projectFilePath, meshBehaviourExporter, true);

            meshBehaviour.ExportMesh(LodMaskLayer.LOD0, meshBehaviourExporter.outputMesh);
            meshBehaviour.ExportMesh(LodMaskLayer.LOD1, meshBehaviourExporter.outputMeshLod1);
            meshBehaviour.ExportMesh(LodMaskLayer.LOD2, meshBehaviourExporter.outputMeshLod2);
            meshBehaviour.ExportMesh(LodMaskLayer.Collider, meshBehaviourExporter.outputMeshForCollider);

            AssetDatabase.SaveAssets();
        }

        static void ExportMeshPrefab(string projectFilePath, MeshBehaviourExporter meshBehaviourExporter)
        {
            if (string.IsNullOrEmpty(projectFilePath))
            {
                throw new OperationCanceledException();
            }

            RefreshMeshReferences(AssetDatabase.GetAssetPath(meshBehaviourExporter.outputMesh), meshBehaviourExporter, false);

            var material = meshBehaviourExporter.GetComponent<MeshRenderer>().sharedMaterial;

            var baseName = Path.GetFileNameWithoutExtension(projectFilePath);
            var prefab = new GameObject(baseName);

            var gameObject0 = new GameObject(baseName + "_LOD0");
            gameObject0.transform.SetParent(prefab.transform, false);
            var meshFilter0 = gameObject0.AddComponent<MeshFilter>();
            meshFilter0.sharedMesh = meshBehaviourExporter.outputMesh;
            var meshRenderer0 = gameObject0.AddComponent<MeshRenderer>();
            meshRenderer0.sharedMaterial = material;

            var gameObject1 = new GameObject(baseName + "_LOD1");
            gameObject1.transform.SetParent(prefab.transform, false);
            var meshFilter1 = gameObject1.AddComponent<MeshFilter>();
            meshFilter1.sharedMesh = meshBehaviourExporter.outputMeshLod1;
            var meshRenderer1 = gameObject1.AddComponent<MeshRenderer>();
            meshRenderer1.sharedMaterial = material;

            var gameObject2 = new GameObject(baseName + "_LOD2");
            gameObject2.transform.SetParent(prefab.transform, false);
            var meshFilter2 = gameObject2.AddComponent<MeshFilter>();
            meshFilter2.sharedMesh = meshBehaviourExporter.outputMeshLod2;
            var meshRenderer2 = gameObject2.AddComponent<MeshRenderer>();
            meshRenderer2.sharedMaterial = material;

            var gameObject9 = new GameObject(baseName + "_Collider");
            gameObject9.transform.SetParent(prefab.transform, false);
            var collider = gameObject9.AddComponent<MeshCollider>();
            collider.sharedMesh = meshBehaviourExporter.outputMeshForCollider;

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