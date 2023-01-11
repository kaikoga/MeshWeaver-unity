using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.UIElements;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

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

        void OnEnable()
        {
            _meshProvider = (StitchMeshProvider)target;

            _serializedPathProviderA = serializedObject.FindProperty(nameof(StitchMeshProvider.pathProviderA));
            _serializedPathProviderB = serializedObject.FindProperty(nameof(StitchMeshProvider.pathProviderB));
            _serializedMaterial = serializedObject.FindProperty(nameof(StitchMeshProvider.material));
        }

        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(new IMGUIContainer(() =>
            {
                MeshWeaverGUILayout.PropertyField(_serializedPathProviderA, Loc("StitchMeshProvider.pathProviderA"));
                PathProviderMenus.CollectionsMenu.PropertyField(_meshProvider, " ", "Path A", ref _meshProvider.pathProviderA);
                MeshWeaverGUILayout.PropertyField(_serializedPathProviderB, Loc("StitchMeshProvider.pathProviderB"));
                PathProviderMenus.CollectionsMenu.PropertyField(_meshProvider, " ", "Path B", ref _meshProvider.pathProviderB);

                MeshWeaverGUILayout.PropertyField(_serializedMaterial, Loc("StitchMeshProvider.material"));
                serializedObject.ApplyModifiedProperties();
            }));
        }

        protected override void PopulateDumpGUI(VisualElement container)
        {
            container.Add(new DumpFoldout(Loc("Path data A"), () => _meshProvider.LastPathieA?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
            container.Add(new DumpFoldout(Loc("Path data B"), () => _meshProvider.LastPathieB?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
        }
    }
}