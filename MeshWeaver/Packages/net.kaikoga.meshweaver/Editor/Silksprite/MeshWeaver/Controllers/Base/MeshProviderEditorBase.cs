using System.Linq;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Meshes;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.GUIActions;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.DataObjects;
using Silksprite.MeshWeaver.Utils;
using UnityEngine;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class MeshProviderEditorBase : ProviderEditorBase
    {
        protected override bool IsMainComponentEditor => true;

        protected sealed override void PopulateInspectorGUI(GUIContainer container)
        {
            container.Add(CreatePropertiesGUI());
            container.Add(MeshModifierProviderMenus.Menu.ToGUIAction((MeshProvider)target));
            container.Add(new LocButton(Loc("Bake"), () =>
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
            }));
            container.Add(CreateDumpGUI());
        }

        GUIAction CreatePropertiesGUI()
        {
            return new Div(c =>
            {
                c.Add(Prop(nameof(MeshProvider.lodMask), Loc("GeometryProvider.lodMask")));
                PopulatePropertiesGUI(c);
            });
        }

        GUIAction CreateDumpGUI()
        {
            return new Div(c =>
            {
                c.Add(new DumpFoldout(Loc("Mesh Dump"), () => ((MeshProvider)target).LastFactory?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
                c.Add(new DumpFoldout(Loc("Collider Mesh Dump"), () => ((MeshProvider)target).LastFactory?.Build(LodMaskLayer.Collider)));
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
            var meshProvider = (MeshProvider)target;
            var globalVertices = meshProvider.ToFactory(NullMeshContext.Instance).Build(LodMaskLayer.Collider)
                .Vertices.Select(v => meshProvider.transform.TransformPoint(v.Vertex));
            return EditorBoundsUtil.CalculateFrameBounds(globalVertices);
        }
    }
}