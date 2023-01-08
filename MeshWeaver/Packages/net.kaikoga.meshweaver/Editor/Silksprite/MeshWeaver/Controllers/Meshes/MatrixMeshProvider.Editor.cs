using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using static Silksprite.MeshWeaver.Utils.Localization;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(MatrixMeshProvider))]
    [CanEditMultipleObjects]
    public class MatrixMeshProviderEditor : MeshProviderEditorBase
    {
        bool _isExpandedX;
        bool _isExpandedY;

        MatrixMeshProvider _meshProvider;

        SerializedProperty _serializedPathProviderX;
        SerializedProperty _serializedPathProviderY;
        SerializedProperty _serializedOperatorKind;
        SerializedProperty _serializedDefaultCellPatternKind;
        SerializedProperty _serializedCellPatternOverrides;
        SerializedProperty _serializedMaterial;

        
        void OnEnable()
        {
            _meshProvider = (MatrixMeshProvider)target;

            _serializedPathProviderX = serializedObject.FindProperty(nameof(MatrixMeshProvider.pathProviderX));
            _serializedPathProviderY = serializedObject.FindProperty(nameof(MatrixMeshProvider.pathProviderY));
            _serializedOperatorKind = serializedObject.FindProperty(nameof(MatrixMeshProvider.operatorKind));
            _serializedDefaultCellPatternKind = serializedObject.FindProperty(nameof(MatrixMeshProvider.defaultCellPatternKind));
            _serializedCellPatternOverrides = serializedObject.FindProperty(nameof(MatrixMeshProvider.cellPatternOverrides));
            _serializedMaterial = serializedObject.FindProperty(nameof(MatrixMeshProvider.material));
        }

        protected override void OnPropertiesGUI()
        {
            MeshWeaverGUILayout.PropertyField(_serializedPathProviderX, Loc("MatrixMeshProvider.pathProviderX"));
            PathProviderMenus.CollectionsMenu.PropertyField(_meshProvider, " ", "Path X", ref _meshProvider.pathProviderX);
            MeshWeaverGUILayout.PropertyField(_serializedPathProviderY, Loc("MatrixMeshProvider.pathProviderY"));
            PathProviderMenus.CollectionsMenu.PropertyField(_meshProvider, " ", "Path Y", ref _meshProvider.pathProviderY);
            
            MeshWeaverGUILayout.PropertyField(_serializedOperatorKind, Loc("MatrixMeshProvider.operatorKind"));
            MeshWeaverGUILayout.PropertyField(_serializedDefaultCellPatternKind, Loc("MatrixMeshProvider.defaultCellPatternKind"));
            MeshWeaverGUILayout.PropertyField(_serializedCellPatternOverrides, Loc("MatrixMeshProvider.cellPatternOverrides"));
            MeshWeaverGUILayout.PropertyField(_serializedMaterial, Loc("MatrixMeshProvider.material"));
            serializedObject.ApplyModifiedProperties();
        }

        protected override void OnDumpGUI()
        {
            base.OnDumpGUI();
            MeshWeaverGUI.DumpFoldout("Path data X", ref _isExpandedX, () => _meshProvider.LastPathieX?.Build(MeshWeaverSettings.Current.currentLodMaskLayer));
            MeshWeaverGUI.DumpFoldout("Path data Y", ref _isExpandedY, () => _meshProvider.LastPathieY?.Build(MeshWeaverSettings.Current.currentLodMaskLayer));
        }
    }
}