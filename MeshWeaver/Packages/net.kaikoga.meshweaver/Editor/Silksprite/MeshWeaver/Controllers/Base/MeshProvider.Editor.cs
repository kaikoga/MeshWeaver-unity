using System.Linq;
using Silksprite.MeshWeaver.Controllers.Context;
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

        Meshie _meshie;
        Meshie _colliderMeshie;

        void OnValidate()
        {
            _meshie = null;
            _colliderMeshie = null;
        }

        public sealed override void OnInspectorGUI()
        {
            OnPropertiesGUI();

            var meshProvider = (MeshProvider)target;
            if (GUILayout.Button("Bake"))
            {
                var transform = meshProvider.transform;
                var baked = transform.parent.AddChildComponent<BakedMeshProvider>();
                using (var context = new DynamicMeshContext())
                {
                    baked.lodMaskLayers = LodMaskLayers.Values;
                    baked.meshData = LodMaskLayers.Values.Select(lod => MeshieData.FromMeshie(meshProvider.ToFactory(context).Build(lod), i => i)).ToArray();
                    baked.materials = context.ToMaterials();
                }
                var bakedTransform = baked.transform;
                bakedTransform.localPosition = transform.localPosition;
                bakedTransform.localRotation = transform.localRotation;
                bakedTransform.localScale = transform.localScale;
            }

            OnDumpGUI();
        }

        protected virtual void OnPropertiesGUI()
        {
            var meshProvider = (MeshProvider)target;
            MeshModifierProviderMenus.Menu.ModifierPopup(meshProvider);

            base.OnInspectorGUI();

        }

        protected virtual void OnDumpGUI()
        {
            var meshProvider = (MeshProvider)target;
            var factory = meshProvider.LastFactory;
            if (factory != null)
            {
                if (_meshie == null) _meshie = factory.Build(MeshWeaverSettings.Current.currentLodMaskLayer);
                if (_colliderMeshie == null) _colliderMeshie = factory.Build(LodMaskLayer.Collider);
            }

            MeshWeaverGUI.DumpFoldout($"Mesh Dump: {_meshie}",
                ref _isExpanded,
                () => _meshie?.Dump());
            MeshWeaverGUI.DumpFoldout($"Collider Mesh Dump: {_colliderMeshie}",
                ref _isColliderExpanded,
                () => _colliderMeshie?.Dump());

        }

        protected bool HasFrameBounds() => true;

        protected Bounds OnGetFrameBounds()
        {
            var meshProvider = (MeshProvider)target;
            var globalVertices = meshProvider.ToFactory(NullMeshContext.Instance).Build(LodMaskLayer.Collider)
                .Vertices.Select(v => meshProvider.transform.TransformPoint(v.Vertex));
            return EditorBoundsUtil.CalculateFrameBounds(globalVertices);
        }
    }
}