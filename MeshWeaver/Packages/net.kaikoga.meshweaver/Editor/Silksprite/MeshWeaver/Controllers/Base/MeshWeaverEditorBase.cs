using System.Collections.Generic;
using Silksprite.MeshWeaver.GUIActions;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Scopes;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;
using static Silksprite.MeshWeaver.Utils.Localization;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class MeshWeaverEditorBase : Editor
    {
        protected abstract bool IsMainComponentEditor { get; }

        GUIContainer _root;
        GUIContainer _container;

        GUIContainer PopulateGUIRoot()
        {
            _root = new GUIContainer();
            PopulateRootElement();

            // NOTE: Not needed because everything is made in IMGUI
            // var onGlobalLangChange = new EventCallback<ChangeEvent<string>>(change =>
            // {
            //     _root = new GUIContainer();
            //     PopulateRootElement();
            // });
            // _root.RegisterCallback(new EventCallback<AttachToPanelEvent>(attach => MeshWeaverSettings.onGlobalLangChange += onGlobalLangChange));
            // _root.RegisterCallback(new EventCallback<DetachFromPanelEvent>(detach => MeshWeaverSettings.onGlobalLangChange -= onGlobalLangChange));

            return _root;

            void PopulateRootElement()
            {
                if (IsMainComponentEditor)
                {
                    _root.Add(GUIAction.Build(() =>
                    {
                        using (var changed = new EditorGUI.ChangeCheckScope())
                        {
                            var list = new List<string> { "en", "ja" };
                            var lang = EditorGUILayout.Popup(Loc("Language (Global)").GUIContent,
                                list.IndexOf(Lang), list.ToArray());
                            if (changed.changed && lang >= 0) Lang = list[lang];
                        }

                        using (var changed = new EditorGUI.ChangeCheckScope())
                        {
                            var lod = EditorGUILayout.EnumPopup(Loc("Current LOD (Global)").GUIContent, MeshWeaverSettings.Current.CurrentLodMaskLayer);
                            if (changed.changed) MeshWeaverSettings.Current.CurrentLodMaskLayer = (LodMaskLayer)lod;
                        }
                    }));
                }

                _container = new GUIContainer();
                PopulateInspectorGUI(_container);
                _root.Add(_container);
            }
        }

        protected abstract void PopulateInspectorGUI(GUIContainer root);

        public sealed override void OnInspectorGUI()
        {
            _root = _root ?? PopulateGUIRoot();
            _root.OnGUI();
        }

        protected GUIAction PopulateDefaultInspectorGUI()
        {
            return GUIAction.Build(() =>
            {
                using (new BackgroundColorScope(Color.magenta))
                {
                    MeshWeaverGUILayout.Header(Loc("Fallback Inspector"));
                    DrawDefaultInspector();
                    MeshWeaverGUILayout.Header(Loc("End Fallback Inspector"));
                }
            });
        }

        protected LocPropertyField Prop(string absolutePath, LocalizedContent loc)
        {
            return new LocPropertyField(serializedObject.FindProperty(absolutePath), loc);
        }
    }
}