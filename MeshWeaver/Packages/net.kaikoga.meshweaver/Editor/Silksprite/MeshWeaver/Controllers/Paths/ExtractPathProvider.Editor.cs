using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    [CustomEditor(typeof(ExtractPathProvider))]
    [CanEditMultipleObjects]
    public class ExtractPathProviderEditor : PathProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(ExtractPathProvider.meshProvider), Loc("ExtractPathProvider.meshProvider")));
            container.Add(Prop(nameof(ExtractPathProvider.pathName), Loc("ExtractPathProvider.pathName")));
        }
    }
}