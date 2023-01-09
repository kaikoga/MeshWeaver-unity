using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using static Silksprite.MeshWeaver.Utils.Localization;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(StitchMeshProvider))]
    [CanEditMultipleObjects]
    public class StitchMeshProviderEditor : MeshProviderEditorBase
    {
        StitchMeshProvider _meshProvider;

        SerializedProperty _serializedPathProviderA;
        SerializedProperty _serializedPathProviderB;
        SerializedProperty _serializedMaterial;

        bool _isExpandedA;
        bool _isExpandedB;

        void OnEnable()
        {
            _meshProvider = (StitchMeshProvider)target;

            _serializedPathProviderA = serializedObject.FindProperty(nameof(StitchMeshProvider.pathProviderA));
            _serializedPathProviderB = serializedObject.FindProperty(nameof(StitchMeshProvider.pathProviderB));
            _serializedMaterial = serializedObject.FindProperty(nameof(StitchMeshProvider.material));
        }

        protected override void OnPropertiesGUI()
        {
            MeshWeaverGUILayout.PropertyField(_serializedPathProviderA, Loc("StitchMeshProvider.pathProviderA"));
            PathProviderMenus.CollectionsMenu.PropertyField(_meshProvider, " ", "Path A", ref _meshProvider.pathProviderA);
            MeshWeaverGUILayout.PropertyField(_serializedPathProviderB, Loc("StitchMeshProvider.pathProviderB"));
            PathProviderMenus.CollectionsMenu.PropertyField(_meshProvider, " ", "Path B", ref _meshProvider.pathProviderB);

            MeshWeaverGUILayout.PropertyField(_serializedMaterial, Loc("StitchMeshProvider.material"));
            serializedObject.ApplyModifiedProperties();
        }

        protected override void OnDumpGUI()
        {
            MeshWeaverGUI.DumpFoldout("Path data A", ref _isExpandedA, () => _meshProvider.LastPathieA?.Build(MeshWeaverSettings.Current.currentLodMaskLayer));
            MeshWeaverGUI.DumpFoldout("Path data B", ref _isExpandedB, () => _meshProvider.LastPathieB?.Build(MeshWeaverSettings.Current.currentLodMaskLayer));
        }
    }
}