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
using static Silksprite.MeshWeaver.Utils.Localization;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class MeshProviderEditorBase : ProviderEditorBase
    {
        bool _isExpanded;
        bool _isColliderExpanded;

        Meshie _meshie;
        Meshie _colliderMeshie;

        public sealed override void OnInspectorGUI()
        {
            MeshWeaverControllerGUILayout.LangSelectorGUI();
            using (var changedScope = new EditorGUI.ChangeCheckScope())
            {
                MeshWeaverControllerGUILayout.LodSelectorGUI(target);
                PropertiesGUI();
                if (changedScope.changed)
                {
                    _meshie = null;
                    _colliderMeshie = null;
                }
            }

            if (GUILayout.Button(Tr("Bake")))
            {
                var meshProvider = (MeshProvider)target;
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

            DumpGUI();
        }

        void PropertiesGUI()
        {
            MeshWeaverGUILayout.PropertyField(serializedObject.FindProperty("lodMask"), Loc("GeometryProvider.lodMask"));
            serializedObject.ApplyModifiedProperties();
            OnPropertiesGUI();
        }

        protected abstract void OnPropertiesGUI();

        void DumpGUI()
        {
            var meshProvider = (MeshProvider)target;
            var factory = meshProvider.LastFactory;
            if (factory != null)
            {
                if (_meshie == null) _meshie = factory.Build(MeshWeaverSettings.Current.currentLodMaskLayer);
                if (_colliderMeshie == null) _colliderMeshie = factory.Build(LodMaskLayer.Collider);
            }

            MeshWeaverGUI.DumpFoldout(Tr("Mesh Dump"), ref _isExpanded, () => _meshie);
            MeshWeaverGUI.DumpFoldout(Tr("Collider Mesh Dump"), ref _isColliderExpanded, () => _colliderMeshie);
            OnDumpGUI();
        }

        protected virtual void OnDumpGUI() { }

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