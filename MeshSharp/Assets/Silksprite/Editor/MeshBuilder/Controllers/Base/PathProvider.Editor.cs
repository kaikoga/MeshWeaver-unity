using Silksprite.MeshBuilder.Controllers.Paths;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models.DataObjects;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    [CustomEditor(typeof(PathProvider), true, isFallback = true)]
    public class PathProviderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var pathProvider = (PathProvider)target;
            base.OnInspectorGUI();
            using (new EditorGUI.DisabledScope(false))
            {
                EditorGUILayout.TextArea(pathProvider.LastPathie?.ToString() ?? "null");
            }

            if (GUILayout.Button("Bake"))
            {
                var transform = pathProvider.transform;
                var baked = transform.parent.AddChildComponent<BakedPathProvider>();
                baked.pathData = PathieData.FromPathie(pathProvider.ToPathie());
                var bakedTransform = baked.transform;
                bakedTransform.localPosition = transform.localPosition;
                bakedTransform.localRotation = transform.localRotation;
                bakedTransform.localScale = transform.localScale;
            }
        }
    }
}