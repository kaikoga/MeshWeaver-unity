using System;

namespace Silksprite.MeshWeaver.GUIActions
{
    public class GUIContainer : GUIAction
    {
        Action _onGUI;

        public void Add(GUIAction element) => _onGUI += element.OnGUI;
        public void AddAction(Action callback) => _onGUI += callback;

        public override void OnGUI()
        {
            OnGUIScope(_onGUI);
        }

        protected virtual void OnGUIScope(Action onGUI)
        {
            onGUI?.Invoke();
        }
    }
}