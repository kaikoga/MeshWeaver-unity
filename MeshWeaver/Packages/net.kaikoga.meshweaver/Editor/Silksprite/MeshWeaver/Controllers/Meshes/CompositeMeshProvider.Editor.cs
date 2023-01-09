using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(CompositeMeshProvider))]
    [CanEditMultipleObjects]
    public class CompositeMeshProviderEditor : MeshProviderEditorBase
    {
        protected override void OnPropertiesGUI()
        {
            var compositeMeshProvider = (CompositeMeshProvider)target;
            MeshProviderMenus.Menu.ChildPopup(compositeMeshProvider, Tr("Mesh Providers"));
        }
    }
}