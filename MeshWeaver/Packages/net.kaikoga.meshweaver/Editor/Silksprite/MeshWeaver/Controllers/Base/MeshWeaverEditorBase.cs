using System;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Scopes;
using Silksprite.MeshWeaver.UIElements;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
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
            if (IsMainComponentEditor)
            {
                _root.Add(new IMGUIContainer(() =>
                {
                    MeshWeaverControllerGUILayout.LangSelectorGUI();
                    MeshWeaverControllerGUILayout.LodSelectorGUI();
                }) { name = "mw-header" } );
            }
            _container = new VisualElement { name = "mw-container" };
            PopulateInspectorGUI(_container);
            _root.Add(_container);
            return _root;
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