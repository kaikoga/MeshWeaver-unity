using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(ImportMeshProvider))]
    [CanEditMultipleObjects]
    public class ImportMeshProviderEditor : MeshProviderEditorBase
    {
        SerializedProperty _serializedMesh;
        SerializedProperty _serializedMaterials;

        void OnEnable()
        {
            _serializedMesh = serializedObject.FindProperty(nameof(ImportMeshProvider.mesh));
            _serializedMaterials = serializedObject.FindProperty(nameof(ImportMeshProvider.materials));
        }

        protected override void OnPropertiesGUI()
        {
            MeshWeaverGUILayout.PropertyField(_serializedMesh, Loc("ImportMeshProvider.mesh"));
            MeshWeaverGUILayout.PropertyField(_serializedMaterials, Loc("ImportMeshProvider.materials"));
            serializedObject.ApplyModifiedProperties();
        }
    }
}