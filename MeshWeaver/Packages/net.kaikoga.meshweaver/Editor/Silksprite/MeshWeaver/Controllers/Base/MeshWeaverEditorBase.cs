using Silksprite.MeshWeaver.GUIActions;
using Silksprite.MeshWeaver.Scopes;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class MeshWeaverEditorBase : Editor
    {
        protected abstract bool IsMainComponentEditor { get; }
        protected virtual bool IsExperimental => false;

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
                if (IsMainComponentEditor) _root.Add(new MainProviderHeader());
                if (IsExperimental) _root.Add(PopulateExperimentalBanner());
                _container = new GUIContainer();
                PopulateInspectorGUI(_container);
                _root.Add(_container);
            }
        }

        GUIAction PopulateExperimentalBanner()
        {
            return new Div(c =>
            {
                c.Add(new Header(Loc("Experimental")));
                c.Add(new LocHelpBox(Loc("This component may break in future release."), MessageType.Warning));
            });
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
                    new Header(Loc("Fallback Inspector")).OnGUI();
                    DrawDefaultInspector();
                    new Header(Loc("End Fallback Inspector")).OnGUI();
                }
            });
        }

        protected LocPropertyField Prop(string absolutePath, LocalizedContent loc)
        {
            return new LocPropertyField(serializedObject.FindProperty(absolutePath), loc);
        }
    }
}