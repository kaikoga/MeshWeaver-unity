using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(MeshReference))]
    [CanEditMultipleObjects]
    public class MeshReferenceEditor : MeshProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(MeshReference.meshProviders), Loc("MeshReference.meshProviders")));
        }
    }
}