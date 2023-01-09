using System;
using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Scopes;
using Silksprite.MeshWeaver.UIElements;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Utils.Localization;
using Random = UnityEngine.Random;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class MeshWeaverEditorBase : Editor
    {
        protected abstract bool IsMainComponentEditor { get; } 

        VisualElement _root;
        VisualElement _container;

        static EventCallback<ChangeEvent<string>> _onGlobalLangChange;

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
            _root.RegisterCallback(new EventCallback<AttachToPanelEvent>(attach => _onGlobalLangChange += onGlobalLangChange));
            _root.RegisterCallback(new EventCallback<DetachFromPanelEvent>(detach => _onGlobalLangChange -= onGlobalLangChange));

            return _root;

            void PopulateRootElement()
            {
                if (IsMainComponentEditor)
                {
                    _root.Add(Div("mw-header", header =>
                    {
                        var langSelector = new PopupField<string>(Tr("Language (Global)"),
                            new List<string> { "en", "ja" },
                            MeshWeaverSettings.Current.lang) { name = "mw-langSelector" };

                        langSelector.RegisterValueChangedCallback(change =>
                        {
                            MeshWeaverSettings.Current.lang = change.newValue;
                            _onGlobalLangChange(change);
                        });
                        header.Add(langSelector);

                        var lodSelector = new EnumField(Tr("Current LOD (Global)"),
                            MeshWeaverSettings.Current.currentLodMaskLayer) { name = "mw-lodSelector" };
                        lodSelector.RegisterValueChangedCallback(change =>
                        {
                            MeshWeaverSettings.Current.currentLodMaskLayer = (LodMaskLayer)change.newValue;
                            EditorApplication.QueuePlayerLoopUpdate();
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