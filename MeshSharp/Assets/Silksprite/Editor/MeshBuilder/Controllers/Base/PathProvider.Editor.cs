using System.Linq;
using Silksprite.MeshBuilder.Controllers.Extensions;
using Silksprite.MeshBuilder.Controllers.Paths;
using Silksprite.MeshBuilder.Controllers.Utils;
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
                () => pathProvider.LastPathie?.Dump());
            MeshBuilderGUI.DumpFoldout($"Collider Path Dump: {pathProvider.LastColliderPathie}",
                ref _isColliderExpanded,
                () => pathProvider.LastColliderPathie?.Dump());

            PathModifierProviderMenus.Menu.ModifierPopup(pathProvider);

            if (GUILayout.Button("Bake"))
            {
                var transform = pathProvider.transform;
                var baked = transform.parent.AddChildComponent<BakedPathProvider>();
                baked.lodMaskLayers = LodMaskLayers.Values;
                baked.pathData = LodMaskLayers.Values.Select(lod => PathieData.FromPathie(pathProvider.ToPathie(lod))).ToArray();
                var bakedTransform = baked.transform;
                bakedTransform.localPosition = transform.localPosition;
                bakedTransform.localRotation = transform.localRotation;
                bakedTransform.localScale = transform.localScale;
            }
        }
    }
}