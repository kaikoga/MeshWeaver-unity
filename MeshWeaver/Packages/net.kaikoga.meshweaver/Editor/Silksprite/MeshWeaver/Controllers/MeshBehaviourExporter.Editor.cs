using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Utils.Localization;

namespace Silksprite.MeshWeaver.Controllers
{
    [CustomEditor(typeof(MeshBehaviourExporter))]
    public class MeshBehaviourExporterEditor : MeshWeaverEditorBase
    {
        MeshBehaviourExporter _meshBehaviourExporter;
        CustomMeshBehaviour _meshBehaviour;

        SerializedProperty _serializedOverrideMaterials;
        SerializedProperty _serializedMaterials;
        SerializedProperty _serializedOutputMesh;
        SerializedProperty _serializedOutputPrefab;
        SerializedProperty _serializedOutputMeshLod1;
        SerializedProperty _serializedOutputMeshLod2;
        SerializedProperty _serializedOutputMeshForCollider;

        bool _editLinkedAssets;

        void OnEnable()
        {
            _meshBehaviourExporter = (MeshBehaviourExporter)target;
            _meshBehaviour = _meshBehaviourExporter.GetComponent<CustomMeshBehaviour>();

            _serializedOverrideMaterials = serializedObject.FindProperty(nameof(MeshBehaviourExporter.overrideMaterials));
            _serializedMaterials = serializedObject.FindProperty(nameof(MeshBehaviourExporter.materials));
            _serializedOutputMesh = serializedObject.FindProperty(nameof(MeshBehaviourExporter.outputMesh));
            _serializedOutputPrefab = serializedObject.FindProperty(nameof(MeshBehaviourExporter.outputPrefab));
            _serializedOutputMeshLod1 = serializedObject.FindProperty(nameof(MeshBehaviourExporter.outputMeshLod1));
            _serializedOutputMeshLod2 = serializedObject.FindProperty(nameof(MeshBehaviourExporter.outputMeshLod2));
            _serializedOutputMeshForCollider = serializedObject.FindProperty(nameof(MeshBehaviourExporter.outputMeshForCollider));
        }

        public sealed override VisualElement CreateInspectorGUI()
        {
            return new IMGUIContainer(OnInspectorIMGUI);
        }

        void OnInspectorIMGUI()
        {
            MeshWeaverControllerGUILayout.LangSelectorGUI();
            MeshWeaverGUILayout.PropertyField(_serializedOverrideMaterials, Loc("MeshBehaviourExporter.overrideMaterials"));
            if (_serializedOverrideMaterials.boolValue)
            {
                MeshWeaverGUILayout.PropertyField(_serializedMaterials, Loc("MeshBehaviourExporter.materials"));
                if (GUILayout.Button(Tr("Copy Materials from MeshBehaviour")))
                {
                    _meshBehaviourExporter.materials = _meshBehaviour.materials.ToArray();
                }
            }

            _editLinkedAssets = EditorGUILayout.Toggle(Loc("Edit Linked Assets").Tr, _editLinkedAssets);

            if (_editLinkedAssets || _serializedOutputMesh.objectReferenceValue)
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    using (new EditorGUI.DisabledScope(!_editLinkedAssets))
                    {
                        MeshWeaverGUILayout.PropertyField(_serializedOutputMesh, Loc("MeshBehaviourExporter.outputMesh"));
                    }
                    if (GUILayout.Button(Tr("Detach")))
                    {
                        _serializedOutputMesh.objectReferenceValue = null;
                    }
                }
            }
            else
            {
                if (GUILayout.Button(Tr("Create Mesh Asset...")))
                {
                    var projectFilePath = EditorUtility.SaveFilePanelInProject(Tr("Export Mesh Asset"),  _meshBehaviourExporter.gameObject.name, "mesh", "");
                    ExportMeshAsset(projectFilePath, _meshBehaviourExporter, _meshBehaviour);
                }
            }

            if (_editLinkedAssets || _serializedOutputPrefab.objectReferenceValue)
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    using (new EditorGUI.DisabledScope(!_editLinkedAssets))
                    {
                        MeshWeaverGUILayout.PropertyField(_serializedOutputPrefab, Loc("MeshBehaviourExporter.outputPrefab"));
                    }
                    if (GUILayout.Button(Tr("Detach")))
                    {
                        _serializedOutputPrefab.objectReferenceValue = null;
                    }
                }
            }
            else
            {
                if (GUILayout.Button(Tr("Create Mesh Prefab...")))
                {
                    var projectFilePath = EditorUtility.SaveFilePanelInProject(Tr("Export Mesh Prefab"), _meshBehaviourExporter.gameObject.name, "prefab", "");
                    ExportMeshPrefab(projectFilePath, _meshBehaviourExporter, _meshBehaviour);
                }
            }

            using (new EditorGUI.DisabledGroupScope(true))
            {
                MeshWeaverGUILayout.PropertyField(_serializedOutputMeshLod1, Loc("MeshBehaviourExporter.outputMeshLod1"));
                MeshWeaverGUILayout.PropertyField(_serializedOutputMeshLod2, Loc("MeshBehaviourExporter.outputMeshLod2"));
                MeshWeaverGUILayout.PropertyField(_serializedOutputMeshForCollider, Loc("MeshBehaviourExporter.outputMeshForCollider"));
            }
            serializedObject.ApplyModifiedProperties();


            if (GUILayout.Button(Tr("Update Exported Assets")))
            {
                if (AssetDatabase.IsMainAsset(_meshBehaviourExporter.outputMesh))
                {
                    var projectFilePath = AssetDatabase.GetAssetPath(_meshBehaviourExporter.outputMesh);
                    ExportMeshAsset(projectFilePath, _meshBehaviourExporter, _meshBehaviour);
                }
                else
                {
                    _meshBehaviourExporter.outputMesh = null;
                }
                if (AssetDatabase.IsMainAsset(_meshBehaviourExporter.outputPrefab))
                {
                    var projectFilePath = AssetDatabase.GetAssetPath(_meshBehaviourExporter.outputPrefab);
                    ExportMeshPrefab(projectFilePath, _meshBehaviourExporter, _meshBehaviour);
                }
                else
                {
                    _meshBehaviourExporter.outputPrefab = null;
                }
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