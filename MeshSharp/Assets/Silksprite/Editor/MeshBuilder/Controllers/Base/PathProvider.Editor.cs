using Silksprite.MeshBuilder.Controllers.Paths;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.DataObjects;
using Silksprite.MeshBuilder.Utils;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    [CustomEditor(typeof(PathProvider), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class PathProviderEditor : Editor
    {
        bool _isExpanded;
        bool _isColliderExpanded;

        public override void OnInspectorGUI()
        {
            var pathProvider = (PathProvider)target;
            base.OnInspectorGUI();

            MeshBuilderGUI.DumpFoldout($"Path Dump: {pathProvider.LastPathie}",
                ref _isExpanded,
                () => pathProvider.LastPathie.Dump());
            MeshBuilderGUI.DumpFoldout($"Collider Path Dump: {pathProvider.LastColliderPathie}",
                ref _isColliderExpanded,
                () => pathProvider.LastColliderPathie.Dump());

            if (GUILayout.Button("Bake"))
            {
                var transform = pathProvider.transform;
                var baked = transform.parent.AddChildComponent<BakedPathProvider>();
                baked.pathData = PathieData.FromPathie(pathProvider.ToPathie(LodMask.LOD0));
                var bakedTransform = baked.transform;
                bakedTransform.localPosition = transform.localPosition;
                bakedTransform.localRotation = transform.localRotation;
                bakedTransform.localScale = transform.localScale;
                baked.RefreshElementsOfParent();
            }
        }
    }
}