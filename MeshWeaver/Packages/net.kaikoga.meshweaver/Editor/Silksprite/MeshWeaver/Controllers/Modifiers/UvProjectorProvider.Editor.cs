using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    [CustomEditor(typeof(UvProjectorProvider))]
    [CanEditMultipleObjects]
    public class UvProjectorProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(UvProjectorProvider.projection), Loc("UvProjectorProvider.projection")));
            container.Add(Prop(nameof(UvProjectorProvider.referenceTranslation), Loc("UvProjectorProvider.referenceTranslation")));
            container.Add(TransformMenus.Menu.ToGUIAction((UvProjectorProvider)target, "Reference Translation",
                serializedObject.FindProperty(nameof(UvProjectorProvider.referenceTranslation))));
            container.Add(Prop(nameof(UvProjectorProvider.axisX), Loc("UvProjectorProvider.axisX")));
            container.Add(Prop(nameof(UvProjectorProvider.axisY), Loc("UvProjectorProvider.axisY")));
            container.Add(Prop(nameof(UvProjectorProvider.uvArea), Loc("UvProjectorProvider.uvArea")));
            container.Add(Prop(nameof(UvProjectorProvider.uvChannel), Loc("UvProjectorProvider.uvChannel")));
        }
    }
}