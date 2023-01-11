using Silksprite.MeshWeaver.Controllers.Base;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    [CustomEditor(typeof(ExtractPathProvider))]
    [CanEditMultipleObjects]
    public class ExtractPathProviderEditor : PathProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(ExtractPathProvider.meshProvider), Loc("ExtractPathProvider.meshProvider")));
            container.Add(Prop(nameof(ExtractPathProvider.pathName), Loc("ExtractPathProvider.pathName")));
        }
    }
}