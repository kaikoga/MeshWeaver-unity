using System;
using System.Linq;
using Silksprite.MeshBuilder.Controllers.Extensions;
using Silksprite.MeshBuilder.Controllers.Meshes;
using Silksprite.MeshBuilder.Controllers.Utils;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.DataObjects;
using Silksprite.MeshBuilder.Utils;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    [CustomEditor(typeof(MeshProvider), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class MeshProviderEditor : Editor
    {
        bool _isExpanded;
        bool _isColliderExpanded;

        public override void OnInspectorGUI()
        {
            var meshProvider = (MeshProvider)target;
            base.OnInspectorGUI();
            MeshBuilderGUI.DumpFoldout($"Mesh Dump: {meshProvider.LastMeshie}",
                ref _isExpanded,
                () => meshProvider.LastMeshie?.Dump());
            MeshBuilderGUI.DumpFoldout($"Collider Mesh Dump: {meshProvider.LastColliderMeshie}",
                ref _isColliderExpanded,
                () => meshProvider.LastColliderMeshie?.Dump());

            MeshModifierProviderMenus.Menu.ModifierPopup(meshProvider);

            if (GUILayout.Button("Bake"))
            {
                var transform = meshProvider.transform;
                var baked = transform.parent.AddChildComponent<BakedMeshProvider>();
                baked.lodMaskLayers = LodMaskLayers.Values;
                baked.meshData = LodMaskLayers.Values.Select(lod => MeshieData.FromMeshie(meshProvider.ToMeshie(lod))).ToArray();
                var bakedTransform = baked.transform;
                bakedTransform.localPosition = transform.localPosition;
                bakedTransform.localRotation = transform.localRotation;
                bakedTransform.localScale = transform.localScale;
            }
        }
    }
}