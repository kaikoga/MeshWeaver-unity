using System;
using System.Collections.Generic;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Scopes;
using Silksprite.MeshWeaver.UIElements;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;
using static Silksprite.MeshWeaver.Utils.Localization;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class MeshWeaverEditorBase : Editor
    {
        protected abstract bool IsMainComponentEditor { get; } 

        VisualElement _root;
        VisualElement _container;

        public sealed override VisualElement CreateInspectorGUI()
        {
            _root = new VisualElement { name = "mw-root" };
            PopulateRootElement();

            var onGlobalLangChange = new EventCallback<ChangeEvent<string>>(change =>
            {
                _root.Unbind();
                _root.Clear();
                PopulateRootElement();
                _root.Bind(serializedObject);
            });
            _root.RegisterCallback(new EventCallback<AttachToPanelEvent>(attach => MeshWeaverSettings.onGlobalLangChange += onGlobalLangChange));
            _root.RegisterCallback(new EventCallback<DetachFromPanelEvent>(detach => MeshWeaverSettings.onGlobalLangChange -= onGlobalLangChange));

            return _root;

            void PopulateRootElement()
            {
                if (IsMainComponentEditor)
                {
                    _root.Add(Div("mw-header", header =>
                    {
                        var langSelector = new PopupField<string>(Tr("Language (Global)"),
                            new List<string> { "en", "ja" },
                            Lang) { name = "mw-langSelector" };

                        langSelector.RegisterValueChangedCallback(change =>
                        {
                            Lang = change.newValue;
                        });
                        header.Add(langSelector);

                        var lodSelector = new EnumField(Tr("Current LOD (Global)"),
                            MeshWeaverSettings.Current.CurrentLodMaskLayer) { name = "mw-lodSelector" };
                        lodSelector.RegisterValueChangedCallback(change =>
                        {
                            MeshWeaverSettings.Current.CurrentLodMaskLayer = (LodMaskLayer)change.newValue;
                        });
                        header.Add(lodSelector);
                    }));
                }
                _container = new VisualElement { name = "mw-container" };
                PopulateInspectorGUI(_container);
                _root.Add(_container);
            }
        }

        protected abstract void PopulateInspectorGUI(VisualElement root);

        public sealed override void OnInspectorGUI() { }

        protected void OnBaseInspectorGUI()
        {
            using (new BackgroundColorScope(Color.magenta))
            {
                MeshWeaverGUILayout.Header(Loc("Fallback Inspector"));
                base.OnInspectorGUI();
                MeshWeaverGUILayout.Header(Loc("End Fallback Inspector"));
            }
        }

        protected static VisualElement Div(string containerName, Action<VisualElement> initializer)
        {
             var container = new VisualElement { name = containerName };
             initializer(container);
             return container;
        }

        protected VisualElement Prop(string absolutePath, LocalizedContent loc)
        {
            return new LocPropertyField(serializedObject.FindProperty(absolutePath), loc);
        }
    }
}