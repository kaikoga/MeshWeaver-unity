using System;
using System.IO;
using System.Linq;
using Silksprite.MeshBuilder.Controllers.Utils;
using Silksprite.MeshBuilder.Models;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers
{
    [CustomEditor(typeof(CustomMeshBehaviour), true, isFallback = true)]
    public class CustomMeshBehaviourEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var meshBehaviour = (CustomMeshBehaviour)target;
            if (meshBehaviour is MeshBehaviour concreteBehaviour)
            {
                MeshProviderMenus.Menu.PropertyField(meshBehaviour, ref concreteBehaviour.meshProviders);
            }
            if (GUILayout.Button("Compile"))
            {
                meshBehaviour.Compile();
            }

            if (GUILayout.Button("Create Mesh Asset"))
            {
                var projectFilePath = EditorUtility.SaveFilePanelInProject("Export Mesh Asset",  meshBehaviour.gameObject.name, "mesh", "");
                ExportMeshAsset(projectFilePath, meshBehaviour);
            }

            if (GUILayout.Button("Update Mesh Asset"))
            {
                if (!AssetDatabase.IsMainAsset(meshBehaviour.outputMesh))
                {
                    throw new InvalidOperationException();
                }
                var projectFilePath = AssetDatabase.GetAssetPath(meshBehaviour.outputMesh);
                ExportMeshAsset(projectFilePath, meshBehaviour);
            }
        }

        static void ExportMeshAsset(string projectFilePath, CustomMeshBehaviour meshBehaviour)
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

            meshBehaviour.ExportMesh(LodMaskLayer.LOD0, mesh0);
            mesh0.name = Path.GetFileNameWithoutExtension(projectFilePath);
            
            meshBehaviour.ExportMesh(LodMaskLayer.LOD1, mesh1);
            mesh1.name = Path.GetFileNameWithoutExtension(projectFilePath) + "_LOD1";

            meshBehaviour.ExportMesh(LodMaskLayer.LOD2, mesh2);
            mesh2.name = Path.GetFileNameWithoutExtension(projectFilePath) + "_LOD2";

            meshBehaviour.ExportMesh(LodMaskLayer.Collider, mesh9);
            mesh9.name = Path.GetFileNameWithoutExtension(projectFilePath) + "_Collider";

            meshBehaviour.outputMesh = mesh0; 

            AssetDatabase.SaveAssets();
        }
    }
}