using Silksprite.MeshBuilder.Controllers.Utils;
using UnityEditor;

namespace Silksprite.MeshBuilder.Controllers.Meshes.Modifiers
{
    [CustomEditor(typeof(MeshRepeatProvider))]
    [CanEditMultipleObjects]
    public class MeshRepeatProviderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var meshRepeatProvider = (MeshRepeatProvider)target;
            PathProviderMenus.VertexMenu.PropertyField(meshRepeatProvider, "Reference Translation", ref meshRepeatProvider.referenceTranslation);
        }
    }
}