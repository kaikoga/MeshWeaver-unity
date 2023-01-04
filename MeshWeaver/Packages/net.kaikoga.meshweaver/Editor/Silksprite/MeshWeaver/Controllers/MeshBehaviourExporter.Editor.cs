using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Scopes;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;
using static Silksprite.MeshWeaver.Utils.Localization;

namespace Silksprite.MeshWeaver.Controllers
{
    [CustomEditor(typeof(MeshBehaviourExporter))]
    public class MeshBehaviourExporterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            using (new BackgroundColorScope(Color.magenta))
            {
                MeshWeaverGUI.Header(Tr("Fallback Inspector"));
                base.OnInspectorGUI();
                MeshWeaverGUI.Header(Tr("End Fallback Inspector"));
            }
            var meshBehaviourExporter = (MeshBehaviourExporter)target;
            var meshBehaviour = meshBehaviourExporter.GetComponent<CustomMeshBehaviour>();
            if (GUILayout.Button(Tr("Copy Materials from MeshBehaviour")))
            {
                meshBehaviourExporter.materials = meshBehaviour.materials.ToArray();
            }

            if (GUILayout.Button(Tr("Create Mesh Asset")))
            {
                var projectFilePath = EditorUtility.SaveFilePanelInProject(Tr("Export Mesh Asset"),  meshBehaviourExporter.gameObject.name, "mesh", "");
                ExportMeshAsset(projectFilePath, meshBehaviourExporter, meshBehaviour);
            }

            if (GUILayout.Button(Tr("Create Mesh Prefab")))
            {
                var projectFilePath = EditorUtility.SaveFilePanelInProject(Tr("Export Mesh Prefab"),  meshBehaviourExporter.gameObject.name, "prefab", "");
                ExportMeshPrefab(projectFilePath, meshBehaviourExporter, meshBehaviour);
            }

            if (GUILayout.Button(Tr("Update Exported Assets")))
            {
                OnValidate();
                if (AssetDatabase.IsMainAsset(meshBehaviourExporter.outputMesh))
                {
                    var projectFilePath = AssetDatabase.GetAssetPath(meshBehaviourExporter.outputMesh);
                    ExportMeshAsset(projectFilePath, meshBehaviourExporter, meshBehaviour);
                }
                if (AssetDatabase.IsMainAsset(meshBehaviourExporter.outputPrefab))
                {
                    var projectFilePath = AssetDatabase.GetAssetPath(meshBehaviourExporter.outputPrefab);
                    ExportMeshPrefab(projectFilePath, meshBehaviourExporter, meshBehaviour);
                }
            }
        }

        void OnValidate()
        {
            var meshBehaviourExporter = (MeshBehaviourExporter)target;
            if (!AssetDatabase.IsMainAsset(meshBehaviourExporter.outputMesh))
            {
                meshBehaviourExporter.outputMesh = null;
            }
            if (!AssetDatabase.IsMainAsset(meshBehaviourExporter.outputPrefab))
            {
                meshBehaviourExporter.outputPrefab = null;
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

            meshBehaviour.ExportMesh(LodMaskLayer.LOD0, meshBehaviourExporter.outputMesh, false);
            meshBehaviour.ExportMesh(LodMaskLayer.LOD1, meshBehaviourExporter.outputMeshLod1, false);
            meshBehaviour.ExportMesh(LodMaskLayer.LOD2, meshBehaviourExporter.outputMeshLod2, false);
            meshBehaviour.ExportMesh(LodMaskLayer.Collider, meshBehaviourExporter.outputMeshForCollider, false);

            AssetDatabase.SaveAssets();
        }

        static void ExportMeshPrefab(string projectFilePath, MeshBehaviourExporter meshBehaviourExporter, CustomMeshBehaviour meshBehaviour)
        {
            if (string.IsNullOrEmpty(projectFilePath))
            {
                throw new OperationCanceledException();
            }

            RefreshMeshReferences(AssetDatabase.GetAssetPath(meshBehaviourExporter.outputMesh), meshBehaviourExporter, false);

            var profileData = meshBehaviour.ProfileData;
            var materials = meshBehaviourExporter.overrideMaterials ? meshBehaviourExporter.materials : meshBehaviour.materials.ToArray();

            var baseName = Path.GetFileNameWithoutExtension(projectFilePath);
            var prefab = new GameObject(baseName);

            MeshRenderer SetupRenderer(GameObject gameObject, Mesh mesh)
            {
                var meshFilter = gameObject.AddComponent<MeshFilter>();
                meshFilter.sharedMesh = mesh;
                var meshRenderer = gameObject.AddComponent<MeshRenderer>();
                meshRenderer.sharedMaterials = materials;
                meshRenderer.receiveGI = profileData.useLightmap ? ReceiveGI.Lightmaps : ReceiveGI.LightProbes;
                return meshRenderer;
            }

            if (!profileData.useLod)
            {
                SetupRenderer(prefab, meshBehaviourExporter.outputMesh);
            }
            else
            {
                MeshRenderer CreateChildRenderer(string name, Mesh mesh)
                {
                    var gameObject = new GameObject(name);
                    gameObject.transform.SetParent(prefab.transform, false);
                    return SetupRenderer(gameObject, mesh);
                }
                
                LOD CreateLOD(Renderer meshRenderer, float screenRelativeTransitionHeight)
                {
                    return new LOD
                    {
                        screenRelativeTransitionHeight = screenRelativeTransitionHeight,
                        fadeTransitionWidth = 0,
                        renderers = new Renderer[] { meshRenderer }
                    };
                }

                var lods = new List<LOD>();

                var meshRenderer0 = CreateChildRenderer(baseName + "_LOD0", meshBehaviourExporter.outputMesh);
                lods.Add(CreateLOD(meshRenderer0, profileData.useLod1 ? 0.6f : profileData.useLod2 ? 0.3f : 0.1f));

                if (profileData.useLod1)
                {
                    var meshRenderer1 = CreateChildRenderer(baseName + "_LOD1", meshBehaviourExporter.outputMeshLod1);
                    lods.Add(CreateLOD(meshRenderer1, profileData.useLod2 ? 0.3f : 0.1f));
                }

                if (profileData.useLod2)
                {
                    var meshRenderer2 = CreateChildRenderer(baseName + "_LOD2", meshBehaviourExporter.outputMeshLod2);
                    lods.Add(CreateLOD(meshRenderer2, 0.1f));
                }

                prefab.AddComponent<LODGroup>().SetLODs(lods.ToArray());
            }

            if (profileData.useCollider)
            {
                var gameObject9 = new GameObject(baseName + "_Collider");
                gameObject9.transform.SetParent(prefab.transform, false);
                var collider = gameObject9.AddComponent<MeshCollider>();
                collider.sharedMesh = meshBehaviourExporter.outputMeshForCollider;
            }

            GameObjectUtility.SetStaticEditorFlags(prefab, profileData.EditorStaticEditorFlags);

            meshBehaviourExporter.outputPrefab = PrefabUtility.SaveAsPrefabAsset(prefab, projectFilePath);
            DestroyImmediate(prefab);
        }
    }
}