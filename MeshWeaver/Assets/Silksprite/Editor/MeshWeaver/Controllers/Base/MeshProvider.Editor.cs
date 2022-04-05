using System.Linq;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Meshes;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.DataObjects;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    [CustomEditor(typeof(MeshProvider), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class MeshProviderEditor : GeometryProviderEditor
    {
        bool _isExpanded;
        bool _isColliderExpanded;

        public override void OnInspectorGUI()
        {
            var meshProvider = (MeshProvider)target;
            base.OnInspectorGUI();
            
            var factory = meshProvider.LastFactory;
            var meshie = factory?.Build(GuessCurrentLodMaskLayer());
            var colliderMeshie = factory?.Build(LodMaskLayer.Collider);

            MeshBuilderGUI.DumpFoldout($"Mesh Dump: {meshie}",
                ref _isExpanded,
                () => meshie?.Dump());
            MeshBuilderGUI.DumpFoldout($"Collider Mesh Dump: {colliderMeshie}",
                ref _isColliderExpanded,
                () => colliderMeshie?.Dump());

            MeshModifierProviderMenus.Menu.ModifierPopup(meshProvider);

            if (GUILayout.Button("Bake"))
            {
                var transform = meshProvider.transform;
                var baked = transform.parent.AddChildComponent<BakedMeshProvider>();
                baked.lodMaskLayers = LodMaskLayers.Values;
                baked.meshData = LodMaskLayers.Values.Select(lod => MeshieData.FromMeshie(meshProvider.ToFactory().Build(lod))).ToArray();
                var bakedTransform = baked.transform;
                bakedTransform.localPosition = transform.localPosition;
                bakedTransform.localRotation = transform.localRotation;
                bakedTransform.localScale = transform.localScale;
            }
        }
    }
}