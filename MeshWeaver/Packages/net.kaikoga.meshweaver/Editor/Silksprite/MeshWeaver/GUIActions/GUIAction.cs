using System;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.GUIActions
{
    public abstract class GUIAction
    {
        public abstract void OnGUI();

        public VisualElement VisualElement() => new IMGUIContainer(OnGUI);

        public static GUIAction Build(Action onGUI) => new GUIActionImpl(onGUI);

        class GUIActionImpl : GUIAction
        {
            readonly Action _onGUI;

            public GUIActionImpl(Action onGUI)
            {
                _onGUI = onGUI;
            }

            public override void OnGUI() => _onGUI();
        }
    }
}