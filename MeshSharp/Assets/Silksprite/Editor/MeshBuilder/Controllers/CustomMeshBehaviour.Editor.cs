using System;
using System.IO;
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

            var mesh = meshBehaviour.ExportMesh(LodMask.LOD0);
            mesh.name = Path.GetFileNameWithoutExtension(projectFilePath);
            meshBehaviour.outputMesh = mesh; 
            AssetDatabase.CreateAsset(mesh, projectFilePath);
        }
    }
}