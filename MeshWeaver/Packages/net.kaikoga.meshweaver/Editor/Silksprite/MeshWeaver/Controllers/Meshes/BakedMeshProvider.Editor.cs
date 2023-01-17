using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(BakedMeshProvider))]
    [CanEditMultipleObjects]
    public class BakedMeshProviderEditor : MeshProviderEditorBase
    {
        protected override bool IsExperimental => true;

        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(BakedMeshProvider.lodMaskLayers), Loc("BakedMeshProvider.lodMaskLayers")));
            container.Add(Prop(nameof(BakedMeshProvider.meshData), Loc("BakedMeshProvider.meshData")));
            container.Add(Prop(nameof(BakedMeshProvider.materials), Loc("BakedMeshProvider.materials")));
        }
    }
}