using System.Linq;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Paths;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.DataObjects;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;
using static Silksprite.MeshWeaver.Utils.Localization;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class PathProviderEditorBase : ProviderEditorBase
    {
        bool _isExpanded;
        bool _isColliderExpanded;
        
        Pathie _pathie;
        Pathie _colliderPathie;

        public sealed override void OnInspectorGUI()
        {
            MeshWeaverControllerGUILayout.LangSelectorGUI();
            using (var changedScope = new EditorGUI.ChangeCheckScope())
            {
                MeshWeaverControllerGUILayout.LodSelectorGUI(target);
                OnPropertiesGUI();
                if (changedScope.changed)
                {
                    _pathie = null;
                    _colliderPathie = null;
                }
            }

            if (GUILayout.Button(Tr("Bake")))
            {
                var pathProvider = (PathProvider)target;
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
            MeshWeaverGUILayout.PropertyField(serializedObject.FindProperty("lodMask"), Loc("GeometryProvider.lodMask"));
            serializedObject.ApplyModifiedProperties();
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

            MeshWeaverGUI.DumpFoldout(Tr("Path Dump"), ref _isExpanded, () => _pathie);
            MeshWeaverGUI.DumpFoldout(Tr("Collider Path Dump"), ref _isColliderExpanded, () => _colliderPathie);

        }

        protected void OnBaseInspectorGUI() => base.OnInspectorGUI();

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