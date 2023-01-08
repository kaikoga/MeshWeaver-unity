using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using static Silksprite.MeshWeaver.Utils.Localization;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(PolygonMeshProvider))]
    [CanEditMultipleObjects]
    public class PolygonMeshProviderEditor : MeshProviderEditorBase
    {
        PolygonMeshProvider _meshProvider;

        SerializedProperty _serializedPathProvider;
        SerializedProperty _serializedMaterial;

        void OnEnable()
        {
            _meshProvider = (PolygonMeshProvider)target;

            _serializedPathProvider = serializedObject.FindProperty(nameof(PolygonMeshProvider.pathProvider));
            _serializedMaterial = serializedObject.FindProperty(nameof(PolygonMeshProvider.material));
        }

        protected override void OnPropertiesGUI()
        {
            base.OnPropertiesGUI();
            MeshWeaverGUILayout.PropertyField(_serializedPathProvider, Loc("PolygonMeshProvider.pathProvider"));
            PathProviderMenus.CollectionsMenu.PropertyField(_meshProvider, " ", "Path", ref _meshProvider.pathProvider);
            MeshWeaverGUILayout.PropertyField(_serializedMaterial, Loc("PolygonMeshProvider.material"));
            serializedObject.ApplyModifiedProperties();
        }
    }
}