using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Controllers.Utils;
using UnityEditor;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    [CustomEditor(typeof(MatrixMeshProvider))]
    public class MatrixMeshProviderEditor : MeshProviderEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var matrixMeshProvider = (MatrixMeshProvider)target;
            PathProviderMenus.CollectionsMenu.PropertyField(matrixMeshProvider, ref matrixMeshProvider.pathProviderX);
            PathProviderMenus.CollectionsMenu.PropertyField(matrixMeshProvider, ref matrixMeshProvider.pathProviderY);

            using (new EditorGUI.DisabledScope(false))
            {
                EditorGUILayout.TextArea(matrixMeshProvider.LastPathieX?.ToString() ?? "null");
                EditorGUILayout.TextArea(matrixMeshProvider.LastPathieY?.ToString() ?? "null");
            }
        }
    }
}