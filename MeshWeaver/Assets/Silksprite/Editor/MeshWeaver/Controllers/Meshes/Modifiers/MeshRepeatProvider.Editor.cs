using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    [CustomEditor(typeof(MeshRepeatProvider))]
    [CanEditMultipleObjects]
    public class MeshRepeatProviderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var meshRepeatProvider = (MeshRepeatProvider)target;
            TransformMenus.Menu.PropertyField(meshRepeatProvider, "Offset By Reference", ref meshRepeatProvider.offsetByReference);
        }
    }
}