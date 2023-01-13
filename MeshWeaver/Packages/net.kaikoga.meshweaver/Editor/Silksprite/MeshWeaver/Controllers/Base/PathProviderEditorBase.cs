using System.Linq;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Paths;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.GUIActions;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.DataObjects;
using Silksprite.MeshWeaver.Utils;
using UnityEngine;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class PathProviderEditorBase : ProviderEditorBase
    {
        protected override bool IsMainComponentEditor => true;

        protected sealed override void PopulateInspectorGUI(GUIContainer container)
        {
            container.Add(CreatePropertiesGUI());
            container.Add(PathModifierProviderMenus.Menu.ToGUIAction((PathProvider)target));
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

        GUIContainer CreatePropertiesGUI()
        {
            return new Div("mw-properties", c =>
            {
                c.Add(Prop(nameof(PathProvider.lodMask), Loc("GeometryProvider.lodMask")));
                PopulatePropertiesGUI(c);
            });
        }

        GUIContainer CreateDumpGUI()
        {
            return new Div("mw-dump", c =>
            {
                c.Add(new DumpFoldout(Loc("Path Dump"), () => ((PathProvider)target).LastFactory?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
                c.Add(new DumpFoldout(Loc("Collider Path Dump"), () => ((PathProvider)target).LastFactory?.Build(LodMaskLayer.Collider)));
                PopulateDumpGUI(c);
            });
        }

        protected abstract void PopulatePropertiesGUI(GUIContainer container);

        protected virtual void PopulateDumpGUI(GUIContainer container)
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