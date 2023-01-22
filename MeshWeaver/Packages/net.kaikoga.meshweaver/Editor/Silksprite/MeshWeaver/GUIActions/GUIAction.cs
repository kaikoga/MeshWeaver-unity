using System;

namespace Silksprite.MeshWeaver.GUIActions
{
    public abstract class GUIAction
    {
        public abstract void OnGUI();

        public static GUIAction Build(Action onGUI) => new GUIActionImpl(onGUI);
        public static GUIAction Scoped(Func<IDisposable> scope, Action<GUIContainer> initializer) => new ScopedImpl(scope, initializer);
        public static GUIAction Scoped(Func<IDisposable> scope, Action onGUI) => new ScopedImpl(scope, onGUI);

        class GUIActionImpl : GUIAction
        {
            readonly Action _onGUI;

            public GUIActionImpl(Action onGUI)
            {
                _onGUI = onGUI;
            }

            public override void OnGUI() => _onGUI();
        }

        class ScopedImpl : GUIContainer
        {
            readonly Func<IDisposable> _scope;

            public ScopedImpl(Func<IDisposable> scope, Action<GUIContainer> initializer)
            {
                _scope = scope;
                initializer(this);
            }

            public ScopedImpl(Func<IDisposable> scope, Action callback)
            {
                _scope = scope;
                AddAction(callback);
            }

            protected override void OnGUIScope(Action onGUI)
            {
                using (_scope())
                {
                    onGUI?.Invoke();
                }
            }
        }
    }
}