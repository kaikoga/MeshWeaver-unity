using System.Linq;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Paths;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.DataObjects;
using Silksprite.MeshWeaver.UIElements;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;
using static Silksprite.MeshWeaver.Utils.Localization;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class PathProviderEditorBase : ProviderEditorBase
    {
        protected override bool IsMainComponentEditor => true;

        bool _isExpanded;
        bool _isColliderExpanded;

        protected sealed override void PopulateInspectorGUI(VisualElement container)
        {
            container.Add(CreatePropertiesGUI());
            container.Add(new LocButton(Loc("Bake"), () =>
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
            }));
            container.Add(CreateDumpGUI());
        }

        VisualElement CreatePropertiesGUI()
        {
            return new Div("mw-properties", c =>
            {
                c.Add(Prop(nameof(PathProvider.lodMask), Loc("GeometryProvider.lodMask")));
                PopulatePropertiesGUI(c);
            });
        }

        VisualElement CreateDumpGUI()
        {
            return new Div("mw-dump", c =>
            {
                c.Add(new IMGUIContainer(() =>
                {
                    var factory = ((PathProvider)target).LastFactory;

                    MeshWeaverGUI.DumpFoldout(Tr("Path Dump"), ref _isExpanded, () => factory?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer));
                    MeshWeaverGUI.DumpFoldout(Tr("Collider Path Dump"), ref _isColliderExpanded, () => factory?.Build(LodMaskLayer.Collider));
                }));
                PopulateDumpGUI(c);
            });
        }

        protected abstract void PopulatePropertiesGUI(VisualElement container);

        protected virtual void PopulateDumpGUI(VisualElement container)
        {
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