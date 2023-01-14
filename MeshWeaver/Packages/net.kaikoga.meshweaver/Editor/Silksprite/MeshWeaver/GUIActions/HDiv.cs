using System;
using UnityEngine;

namespace Silksprite.MeshWeaver.GUIActions
{
    public class HDiv : GUIContainer
    {
        public HDiv(Action<GUIContainer> initializer)
        {
            initializer(this);
        }

        protected override void OnGUIScope(Action onGUI)
        {
            using (new GUILayout.HorizontalScope())
            {
                onGUI?.Invoke();
            }
        }
    }
}