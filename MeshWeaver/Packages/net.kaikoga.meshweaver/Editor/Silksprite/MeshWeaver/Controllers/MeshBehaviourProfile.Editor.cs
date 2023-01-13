using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.Controllers
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MeshBehaviourProfile))]
    public class MeshBehaviourProfileEditor : MeshWeaverEditorBase
    {
        protected override bool IsMainComponentEditor => false;

        protected sealed override void PopulateInspectorGUI(GUIContainer container)
        {
            container.Add(GUIAction.Build(() =>
            {
                var property = serializedObject.FindProperty("data");
                property.NextVisible(true);
                do
                {
                    EditorGUILayout.PropertyField(property);
                } while (property.NextVisible(false));

                serializedObject.ApplyModifiedProperties();
            }));
        }
    }
}