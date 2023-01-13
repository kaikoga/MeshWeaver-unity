using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(CompositeMeshProvider))]
    [CanEditMultipleObjects]
    public class MeshReferenceEditor : MeshProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
        }
    }
}