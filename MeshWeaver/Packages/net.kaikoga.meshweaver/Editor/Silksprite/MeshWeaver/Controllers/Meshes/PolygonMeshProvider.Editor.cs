using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(PolygonMeshProvider))]
    [CanEditMultipleObjects]
    public class PolygonMeshProviderEditor : MeshProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(new Header(Loc("Sources")));
            container.Add(Prop(nameof(PolygonMeshProvider.pathProvider), Loc("PolygonMeshProvider.pathProvider")));
            container.Add(PathProviderMenus.CollectionsMenu.ToGUIAction((PolygonMeshProvider)target, "Path",
                serializedObject.FindProperty(nameof(PolygonMeshProvider.pathProvider))));

            container.Add(new Header(Loc("Output")));
            container.Add(Prop(nameof(PolygonMeshProvider.material), Loc("PolygonMeshProvider.material")));
        }
    }
}