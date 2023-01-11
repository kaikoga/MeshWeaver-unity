using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(PolygonMeshProvider))]
    [CanEditMultipleObjects]
    public class PolygonMeshProviderEditor : MeshProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(PolygonMeshProvider.pathProvider), Loc("PolygonMeshProvider.pathProvider")));
            container.Add(PathProviderMenus.CollectionsMenu.VisualElement(((PolygonMeshProvider)target), " ", "Path",
                serializedObject.FindProperty(nameof(PolygonMeshProvider.pathProvider))));
            container.Add(Prop(nameof(PolygonMeshProvider.material), Loc("PolygonMeshProvider.material")));
        }
    }
}