using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using Silksprite.MeshWeaver.GUIActions.Events;
using Silksprite.MeshWeaver.GUIActions.Extensions;
using Silksprite.MeshWeaver.Models;
using UnityEditor;
using UnityEngine;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers
{
    [CustomEditor(typeof(MeshBehaviourExporter))]
    public class MeshBehaviourExporterEditor : MeshWeaverEditorBase
    {
        protected override bool IsMainComponentEditor => false;

        MeshBehaviourExporter _meshBehaviourExporter;
        MeshBehaviourBase _meshBehaviour;

        bool _editLinkedAssets;

        void OnEnable()
        {
            _meshBehaviourExporter = (MeshBehaviourExporter)target;
            _meshBehaviour = _meshBehaviourExporter.GetComponent<MeshBehaviourBase>();
        }

        protected sealed override void PopulateInspectorGUI(GUIContainer container)
        {
            var onRefresh = new Dispatcher<RefreshEvent>();

            var overrideMaterials = Prop(nameof(MeshBehaviourExporter.overrideMaterials), Loc("MeshBehaviourExporter.overrideMaterials"));
            overrideMaterials.RegisterPropertyChangedCallback<bool>(_ => onRefresh.Invoke());
            container.Add(overrideMaterials);

            var materialsContainer = new Div(c =>
            {
                c.Add(Prop(nameof(MeshBehaviourExporter.materials), Loc("MeshBehaviourExporter.materials")));
                c.Add(new LocButton(Loc("Copy Materials from MeshBehaviour"), () => { _meshBehaviourExporter.materials = _meshBehaviour.materials.ToArray(); }));
            }).WithDisplayOnRefresh(onRefresh, () => _meshBehaviourExporter.overrideMaterials);
            container.Add(materialsContainer);

            var editLinkedAssets = new LocToggle(Loc("Edit Linked Assets")) { value = _editLinkedAssets };
            editLinkedAssets.RegisterValueChangedCallback(changed =>
            {
                _editLinkedAssets = editLinkedAssets.value;
                onRefresh.Invoke();
            });
            container.Add(editLinkedAssets);

            var outputMeshContainer = new DivIfElse(
                new HDiv(c =>
                {
                    var outputMesh = Prop(nameof(MeshBehaviourExporter.outputMesh), Loc("MeshBehaviourExporter.outputMesh"))
                        .WithEnableOnRefresh(onRefresh, () => _editLinkedAssets);
                    c.Add(outputMesh);
                    c.Add(new LocButton(Loc("Detach"), () =>
                    {
                        _meshBehaviourExporter.outputMesh = null;
                        onRefresh.Invoke();
                    }));
                }),
                new LocButton(Loc("Create Mesh Asset..."), () =>
                {
                    var projectFilePath = EditorUtility.SaveFilePanelInProject(Tr("Export Mesh Asset"), _meshBehaviourExporter.gameObject.name, "mesh", "");
                    ExportMeshAsset(projectFilePath, _meshBehaviourExporter, _meshBehaviour);
                    onRefresh.Invoke();
                })).WithDisplayOnRefresh(onRefresh, () => _editLinkedAssets || _meshBehaviourExporter.outputMesh);
            container.Add(outputMeshContainer);

            var outputPrefabContainer = new DivIfElse(
                new HDiv(c =>
                {
                    var outputPrefab = Prop(nameof(MeshBehaviourExporter.outputPrefab), Loc("MeshBehaviourExporter.outputPrefab"))
                        .WithEnableOnRefresh(onRefresh, () => _editLinkedAssets);
                    c.Add(outputPrefab);
                    c.Add(new LocButton(Loc("Detach"), () =>
                    {
                        _meshBehaviourExporter.outputPrefab = null;
                        onRefresh.Invoke();
                    }));
                }),
                new LocButton(Loc("Create Mesh Prefab..."), () =>
                {
                    var projectFilePath = EditorUtility.SaveFilePanelInProject(Tr("Export Mesh Prefab"), _meshBehaviourExporter.gameObject.name, "prefab", "");
                    ExportMeshPrefab(projectFilePath, _meshBehaviourExporter, _meshBehaviour);
                    onRefresh.Invoke();
                })).WithDisplayOnRefresh(onRefresh, () => _editLinkedAssets || _meshBehaviourExporter.outputPrefab);
            container.Add(outputPrefabContainer);

            container.Add(new Div(c =>
            {
                c.Add(Prop(nameof(MeshBehaviourExporter.outputMeshLod1), Loc("MeshBehaviourExporter.outputMeshLod1")).WithEnabled(false));
                c.Add(Prop(nameof(MeshBehaviourExporter.outputMeshLod2), Loc("MeshBehaviourExporter.outputMeshLod2")).WithEnabled(false));
                c.Add(Prop(nameof(MeshBehaviourExporter.outputMeshForCollider), Loc("MeshBehaviourExporter.outputMeshForCollider")).WithEnabled(false));
            }));

            container.Add(new LocButton(Loc("Update Exported Assets"), () =>
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
            }));

            onRefresh.Invoke();
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

        static void ExportMeshAsset(string projectFilePath, MeshBehaviourExporter meshBehaviourExporter, MeshBehaviourBase meshBehaviour)
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

        static void ExportMeshPrefab(string projectFilePath, MeshBehaviourExporter meshBehaviourExporter, MeshBehaviourBase meshBehaviour)
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