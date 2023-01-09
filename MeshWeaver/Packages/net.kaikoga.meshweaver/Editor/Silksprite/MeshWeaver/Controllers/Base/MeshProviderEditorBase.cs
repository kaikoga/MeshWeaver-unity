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
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Utils.Localization;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class MeshProviderEditorBase : ProviderEditorBase
    {
        bool _isExpanded;
        bool _isColliderExpanded;

        public sealed override VisualElement CreateInspectorGUI()
        {
            var container = CreateRootContainerElement();
            container.Add(new IMGUIContainer(() =>
            {
                MeshWeaverControllerGUILayout.LangSelectorGUI();
                MeshWeaverControllerGUILayout.LodSelectorGUI(target);
            }));
            container.Add(new IMGUIContainer(PropertiesGUI));
            container.Add(new IMGUIContainer(() =>
            {
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
            }));
            container.Add(new IMGUIContainer(DumpGUI));
            return container;
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
            var factory = ((MeshProvider)target).LastFactory;
            MeshWeaverGUI.DumpFoldout(Tr("Mesh Dump"), ref _isExpanded, () => factory?.Build(MeshWeaverSettings.Current.currentLodMaskLayer));
            MeshWeaverGUI.DumpFoldout(Tr("Collider Mesh Dump"), ref _isColliderExpanded, () => factory?.Build(LodMaskLayer.Collider));
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