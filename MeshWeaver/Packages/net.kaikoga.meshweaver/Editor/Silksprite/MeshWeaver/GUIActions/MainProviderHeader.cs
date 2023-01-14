using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Scopes;
using Silksprite.MeshWeaver.Tools;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.GUIActions
{
    public class MainProviderHeader : GUIAction
    {
        const float HeaderPopupWidth = 160f;
        const float HeaderLabelWidth = 100f;

        public override void OnGUI()
        {
            using (new BoxLayoutScope(MeshWeaverSkin.Primary))
            using (new LabelWidthScope(HeaderLabelWidth))
            {
                var height = EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
                var rect = GUILayoutUtility.GetRect(0f, float.MaxValue, height, height);
                var labelRect = rect;
                labelRect.width -= HeaderPopupWidth;
                GUI.Label(labelRect, $"MeshWeaver\n{MeshWeaverConstants.Version}", MeshWeaverSkin.Header);

                rect.xMin = rect.xMax - HeaderPopupWidth;
                var popupRect = rect;
                popupRect.height = EditorGUIUtility.singleLineHeight;
                using (var changed = new EditorGUI.ChangeCheckScope())
                {
                    var list = new List<string> { "en", "ja" };
                    var lang = EditorGUI.Popup(popupRect, LocalizationTool.Loc("Language").Tr, list.IndexOf(Localization.Lang), list.ToArray());
                    if (changed.changed && lang >= 0) Localization.Lang = list[lang];
                }

                popupRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                using (var changed = new EditorGUI.ChangeCheckScope())
                {
                    var lod = EditorGUI.EnumPopup(popupRect, LocalizationTool.Loc("Current LOD").Tr, MeshWeaverSettings.Current.CurrentLodMaskLayer);
                    if (changed.changed) MeshWeaverSettings.Current.CurrentLodMaskLayer = (LodMaskLayer)lod;
                }
            }
        }
    }
}