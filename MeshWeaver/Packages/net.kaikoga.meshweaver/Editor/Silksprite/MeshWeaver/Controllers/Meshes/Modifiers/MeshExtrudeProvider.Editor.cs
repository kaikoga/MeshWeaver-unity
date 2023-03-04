using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    [CustomEditor(typeof(MeshExtrudeProvider))]
    [CanEditMultipleObjects]
    public class MeshExtrudeProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(MeshExtrudeProvider.fillBody), Loc("MeshExtrudeProvider.fillBody")));
            container.Add(Prop(nameof(MeshExtrudeProvider.fillBottom), Loc("MeshExtrudeProvider.fillBottom")));
            container.Add(Prop(nameof(MeshExtrudeProvider.fillTop), Loc("MeshExtrudeProvider.fillTop")));
            container.Add(Prop(nameof(MeshExtrudeProvider.reverseLids), Loc("MeshExtrudeProvider.reverseLids")));
            container.Add(Prop(nameof(MeshExtrudeProvider.isSmooth), Loc("MeshExtrudeProvider.isSmooth")));
            container.Add(Prop(nameof(MeshExtrudeProvider.isFromCenter), Loc("MeshExtrudeProvider.isFromCenter")));
            container.Add(Prop(nameof(MeshExtrudeProvider.offset), Loc("MeshExtrudeProvider.offset")));
            container.Add(Prop(nameof(MeshExtrudeProvider.offsetByReference), Loc("MeshExtrudeProvider.offsetByReference")));
            container.Add(TransformMenus.Menu.ToGUIAction((MeshExtrudeProvider)target, "Offset By Reference",
                serializedObject.FindProperty(nameof(MeshExtrudeProvider.offsetByReference))));
        }
    }
}