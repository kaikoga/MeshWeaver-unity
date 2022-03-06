using Silksprite.MeshBuilder.Controllers.Meshes;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models.DataObjects;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    [CustomEditor(typeof(MeshProvider), true, isFallback = true)]
    public class MeshProviderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var meshProvider = (MeshProvider)target;
            base.OnInspectorGUI();
            using (new EditorGUI.DisabledScope(false))
            {
                EditorGUILayout.TextArea(meshProvider.LastMeshie?.ToString() ?? "null");
            }

            if (GUILayout.Button("Bake"))
            {
                var transform = meshProvider.transform;
                var baked = transform.parent.AddChildComponent<BakedMeshProvider>();
                baked.meshData = MeshieData.FromMeshie(meshProvider.ToMeshie());
                var bakedTransform = baked.transform;
                bakedTransform.localPosition = transform.localPosition;
                bakedTransform.localRotation = transform.localRotation;
                bakedTransform.localScale = transform.localScale;
                baked.RefreshElementsOfParent();
            }
        }
    }
}