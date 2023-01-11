using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Meshes.Modifiers;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    [CustomEditor(typeof(VertwiseTranslateProvider))]
    [CanEditMultipleObjects]
    public class VertwiseTranslateProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(VertwiseTranslateProvider.size), Loc("VertwiseTranslateProvider.size")));
            container.Add(Prop(nameof(VertwiseTranslateProvider.referenceTranslation), Loc("VertwiseTranslateProvider.referenceTranslation")));
            container.Add(TransformMenus.Menu.VisualElement((VertwiseTranslateProvider)target, "Reference Translation",
                serializedObject.FindProperty(nameof(VertwiseTranslateProvider.referenceTranslation))));
        }
    }
}