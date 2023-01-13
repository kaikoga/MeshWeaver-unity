using Silksprite.MeshWeaver.GUIActions.Events;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.GUIActions
{
    public class LocToggle : GUIAction
    {
        readonly LocalizedContent _loc;
        public bool value;

        public readonly Dispatcher<ChangeEvent<bool>> onValueChanged = new Dispatcher<ChangeEvent<bool>>();

        public LocToggle(LocalizedContent loc)
        {
            _loc = loc;
        }

        public override void OnGUI()
        {
            EditorGUI.BeginChangeCheck();

            value = GUILayout.Toggle(value, _loc.GUIContent);

            if (EditorGUI.EndChangeCheck())
            {
                onValueChanged.Invoke();
            }
        }
    }
}