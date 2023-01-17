using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    [CustomEditor(typeof(BakedPathProvider))]
    [CanEditMultipleObjects]
    public class BakedPathProviderEditor : PathProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(BakedPathProvider.lodMaskLayers), Loc("BakedPathProvider.lodMaskLayers")));
            container.Add(Prop(nameof(BakedPathProvider.pathData), Loc("BakedPathProvider.pathData")));
        }
    }
}