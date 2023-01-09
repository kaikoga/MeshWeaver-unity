using Silksprite.MeshWeaver.Controllers.Base;
using UnityEditor;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.Controllers
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MeshBehaviourProfile))]
    public class MeshBehaviourProfileEditor : MeshWeaverEditorBase
    {
        public sealed override VisualElement CreateInspectorGUI()
        {
            return new IMGUIContainer(OnInspectorIMGUI);
        }

        void OnInspectorIMGUI()
        {
            var serializedObj = serializedObject;
            var property = serializedObject.FindProperty("data");
            property.NextVisible(true);
            do
            {
                EditorGUILayout.PropertyField(property);
            } while (property.NextVisible(false));
            serializedObj.ApplyModifiedProperties();
        }
    }
}