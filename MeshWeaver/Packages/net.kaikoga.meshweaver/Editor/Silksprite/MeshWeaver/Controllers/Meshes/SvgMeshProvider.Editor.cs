using System.IO;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(SvgMeshProvider))]
    [CanEditMultipleObjects]
    public class SvgMeshProviderEditor : MeshProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(SvgMeshProvider.svg), Loc("SvgMeshProvider.svg")));
            container.Add(Prop(nameof(SvgMeshProvider.svgSprite), Loc("SvgMeshProvider.svgSprite")));

            container.Add(new LocButton(Loc("Reload SVG from Sprite"), () =>
            {
                var svgMeshProvider = (SvgMeshProvider)target;
                svgMeshProvider.svg = new StreamReader(AssetDatabase.GetAssetPath(svgMeshProvider.svgSprite)).ReadToEnd();
                EditorUtility.SetDirty(svgMeshProvider);
            }));
            
            container.Add(Prop(nameof(SvgMeshProvider.material), Loc("SvgMeshProvider.material")));
        }
    }
}