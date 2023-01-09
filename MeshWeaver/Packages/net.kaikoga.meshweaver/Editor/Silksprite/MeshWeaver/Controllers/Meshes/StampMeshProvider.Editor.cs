using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Utils.Localization;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(StampMeshProvider))]
    [CanEditMultipleObjects]
    public class StampMeshProviderEditor : MeshProviderEditorBase
    {
        StampMeshProvider _meshProvider;

        SerializedProperty _serializedMeshProvider;
        SerializedProperty _serializedPathProvider;

        bool _isExpandedMesh;
        bool _isExpandedPath;

        void OnEnable()
        {
            _meshProvider = (StampMeshProvider)target;

            _serializedMeshProvider = serializedObject.FindProperty(nameof(StampMeshProvider.meshProvider));
            _serializedPathProvider = serializedObject.FindProperty(nameof(StampMeshProvider.pathProvider));
        }

        protected override void OnPropertiesGUI()
        {
            MeshWeaverGUILayout.PropertyField(_serializedMeshProvider, Loc("StampMeshProvider.meshProvider"));
            MeshProviderMenus.Menu.PropertyField(_meshProvider, " ", "Mesh", ref _meshProvider.meshProvider);
            MeshWeaverGUILayout.PropertyField(_serializedPathProvider, Loc("StampMeshProvider.pathProvider"));
            PathProviderMenus.ElementsMenu.PropertyField(_meshProvider, " ", "Path", ref _meshProvider.pathProvider);
            serializedObject.ApplyModifiedProperties();
        }

        protected override void OnDumpGUI()
        {
            MeshWeaverGUI.DumpFoldout("Mesh data", ref _isExpandedMesh, () => _meshProvider.LastMeshie?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer));
            MeshWeaverGUI.DumpFoldout("Path data", ref _isExpandedPath, () => _meshProvider.LastPathie?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer));
        }
    }
}