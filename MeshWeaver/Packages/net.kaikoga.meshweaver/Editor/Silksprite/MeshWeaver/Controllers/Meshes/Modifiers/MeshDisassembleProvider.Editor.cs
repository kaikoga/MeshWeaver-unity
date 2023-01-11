using Silksprite.MeshWeaver.Controllers.Base;
using UnityEditor;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    [CustomEditor(typeof(MeshDisassembleProvider))]
    [CanEditMultipleObjects]
    public class MeshDisassembleProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
        }
    }
}