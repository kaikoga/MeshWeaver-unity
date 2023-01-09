using Silksprite.MeshWeaver.Controllers.Base;
using UnityEditor;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.Controllers
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MeshBehaviourProfile))]
    public class MeshBehaviourProfileEditor : MeshWeaverEditorBase
    {
        protected override bool IsMainComponentEditor => false;

        protected sealed override void PopulateInspectorGUI(VisualElement container)
        {
            container.Add(new IMGUIContainer(() =>
            {
                var serializedObj = serializedObject;
                var property = serializedObject.FindProperty("data");
                property.NextVisible(true);
                do
                {
                    EditorGUILayout.PropertyField(property);
                } while (property.NextVisible(false));
                serializedObj.ApplyModifiedProperties();
            }));
        }
    }
}