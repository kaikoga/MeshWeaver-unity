using System.Linq;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Paths;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.DataObjects;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    [CustomEditor(typeof(PathProvider), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class PathProviderEditor : GeometryProviderEditor
    {
        bool _isExpanded;
        bool _isColliderExpanded;
        
        Pathie _pathie;
        Pathie _colliderPathie;

        void OnValidate()
        {
            _pathie = null;
            _colliderPathie = null;
        }

        public sealed override void OnInspectorGUI()
        {
            OnPropertiesGUI();
            var pathProvider = (PathProvider)target;

            if (GUILayout.Button("Bake"))
            {
                var transform = pathProvider.transform;
                var baked = transform.parent.AddChildComponent<BakedPathProvider>();
                baked.lodMaskLayers = LodMaskLayers.Values;
                baked.pathData = LodMaskLayers.Values.Select(lod => PathieData.FromPathie(pathProvider.ToFactory().Build(lod))).ToArray();
                var bakedTransform = baked.transform;
                bakedTransform.localPosition = transform.localPosition;
                bakedTransform.localRotation = transform.localRotation;
                bakedTransform.localScale = transform.localScale;
            }

            OnDumpGUI();
        }

        protected virtual void OnPropertiesGUI()
        {
            var pathProvider = (PathProvider)target;
            base.OnInspectorGUI();

            PathModifierProviderMenus.Menu.ModifierPopup(pathProvider);
        }

        protected virtual void OnDumpGUI()
        {
            var pathProvider = (PathProvider)target;
            var factory = pathProvider.LastFactory;
            if (factory != null)
            {
                if (_pathie == null) _pathie = factory.Build(MeshWeaverSettings.Current.currentLodMaskLayer);
                if (_colliderPathie == null) _colliderPathie = factory.Build(LodMaskLayer.Collider);
            }

            MeshWeaverGUI.DumpFoldout($"Path Dump: {_pathie}",
                ref _isExpanded,
                () => _pathie?.Dump());
            MeshWeaverGUI.DumpFoldout($"Collider Path Dump: {_colliderPathie}",
                ref _isColliderExpanded,
                () => _colliderPathie?.Dump());

        }

        protected bool HasFrameBounds() => true;

        protected Bounds OnGetFrameBounds()
        {
            var pathProvider = (PathProvider)target;
            var globalVertices = pathProvider.ToFactory().Build(LodMaskLayer.Collider)
                .Vertices.Select(v => pathProvider.transform.TransformPoint(v.Vertex));
            return EditorBoundsUtil.CalculateFrameBounds(globalVertices);
        }
    }
}