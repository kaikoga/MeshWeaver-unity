using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Controllers.Commands;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Controllers.Paths;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.GUIActions;
using Silksprite.MeshWeaver.GUIActions.Extensions;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Scopes;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
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
            if (MeshWeaverSettings.Current.enableDumpGUI) container.Add(CreateDumpGUI());
            if (MeshWeaverSettings.Current.enableAdvancedActions) container.Add(CreateAdvancedActionsGUI());
        }

        GUIAction CreatePropertiesGUI()
        {
            return new Div(c =>
            {
                c.Add(Prop(nameof(PathProvider.lodMask), Loc("GeometryProvider.lodMask")));
                PopulatePropertiesGUI(c);
            });
        }

        GUIAction CreateDumpGUI()
        {
            var div = new Div(c =>
            {
                c.Add(new Header(Loc("Dumps")));
                c.Add(new DumpFoldout(Loc("Path Dump"), () => ((PathProvider)target).LastFactory?.Build(MeshWeaverSettings.Current.activeLodMaskLayer)));
                c.Add(new DumpFoldout(Loc("Collider Path Dump"), () => ((PathProvider)target).LastFactory?.Build(LodMaskLayer.Collider)));
                PopulateDumpGUI(c);
            });
            return GUIAction.Build(() =>
            {
                using (new EditorGUI.IndentLevelScope())
                using (new BoxLayoutScope(MeshWeaverSkin.Dump))
                {
                    div.OnGUI();
                }
            });
        }

        GUIAction CreateAdvancedActionsGUI()
        {
            var menuItems = new List<LocMenuItem>();
            PopulateAdvancedActions(menuItems);
            PopulateCommonAdvancedActions(menuItems);
            return new LocPopupButtons(Loc("Advanced Actions"), Loc("Command..."), menuItems.ToArray());
        }

        protected abstract void PopulatePropertiesGUI(GUIContainer container);

        protected virtual void PopulateDumpGUI(GUIContainer container)
        {
        }

        protected virtual void PopulateAdvancedActions(List<LocMenuItem> menuItems)
        {
        }

        void PopulateCommonAdvancedActions(List<LocMenuItem> menuItems)
        {
            menuItems.Add(new UpgradeByWrapWithCompositeEntirely<PathProvider, CompositePathProvider>().ToLocMenuItem(target as PathProvider));
            menuItems.Add(new BakePath().ToLocMenuItem((PathProvider)target));
        }

        protected bool HasFrameBounds() => true;

        protected Bounds OnGetFrameBounds()
        {
            var pathProvider = (PathProvider)target;
            var globalVertices = pathProvider.ToFactory(NullMeshContext.Instance).Build(LodMaskLayer.Collider)
                .Vertices.Select(v => pathProvider.transform.TransformPoint(v.Vertex));
            return EditorBoundsUtil.CalculateFrameBounds(globalVertices);
        }
    }
}