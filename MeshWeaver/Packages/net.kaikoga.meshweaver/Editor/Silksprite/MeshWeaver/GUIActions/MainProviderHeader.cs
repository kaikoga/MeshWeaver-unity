using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Scopes;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.GUIActions
{
    public class MainProviderHeader : GUIAction
    {
        static bool _acknowledgeSettingsAssetCreated;

        const float HeaderPopupWidth = 160f;
        const float HeaderLabelWidth = 100f;

        static Texture2D Banner => AssetDatabase.LoadAssetAtPath<Texture2D>("Packages/net.kaikoga.meshweaver/Editor/Images/banner.png");

        bool _isExpanded;
        readonly bool _showFoldout;

        public MainProviderHeader(bool isExpanded, bool showFoldout)
        {
            _isExpanded = isExpanded;
            _showFoldout = showFoldout;
            if (_acknowledgeSettingsAssetCreated)
            {
                MeshWeaverSettings.InfoSettingsAssetCreated = false;
                _acknowledgeSettingsAssetCreated = false;
            }
        }

        public override void OnGUI()
        {
            using (new BoxLayoutScope(MeshWeaverSkin.Primary))
            using (new LabelWidthScope(HeaderLabelWidth))
            {
                var height = _isExpanded ? EditorGUIUtility.singleLineHeight * 3 + EditorGUIUtility.standardVerticalSpacing * 2 : EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
                var rect = GUILayoutUtility.GetRect(0f, float.MaxValue, height, height);
                var leftPane = rect;
                leftPane.xMin += 10f;
                leftPane.width -= HeaderPopupWidth + 10f;
                var rightPane = rect;
                rightPane.xMin = rect.xMax - HeaderPopupWidth;

                var labelRect = leftPane;
                labelRect.height = EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
                GUI.DrawTexture(labelRect, Banner, ScaleMode.ScaleToFit);

                if (_showFoldout)
                {
                    var foldoutRect = labelRect;
                    foldoutRect.width = 0f;
                    _isExpanded = EditorGUI.Foldout(foldoutRect, _isExpanded, "");
                }

                var popupRect = rightPane;
                popupRect.height = EditorGUIUtility.singleLineHeight;
                using (var changed = new EditorGUI.ChangeCheckScope())
                {
                    var list = new List<string> { "en", "ja" };
                    var lang = EditorGUI.Popup(popupRect, Loc("Language").Tr, list.IndexOf(Localization.Lang), list.ToArray());
                    if (changed.changed && lang >= 0) Localization.Lang = list[lang];
                }

                popupRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                using (var changed = new EditorGUI.ChangeCheckScope())
                {
                    var lod = EditorGUI.EnumPopup(popupRect, Loc("Current LOD").Tr, MeshWeaverSettings.Current.CurrentLodMaskLayer);
                    if (changed.changed)
                    {
                        MeshWeaverSettings.Current.CurrentLodMaskLayer = (LodMaskLayer)lod;
                    }
                }

                if (_isExpanded)
                {
                    var versionRect = new Rect(leftPane.x, EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing * 2, leftPane.width, EditorGUIUtility.singleLineHeight);
                    GUI.Label(versionRect, $"MeshWeaver {MeshWeaverConstants.Version}");
                }
                
                if (MeshWeaverSettings.WarnMultipleSettingsAsset)
                {
                    new LocHelpBox(Loc("Multiple MeshWeaver Settings asset found. Settings may or may not be saved."), MessageType.Warning).OnGUI();
                }
                if (MeshWeaverSettings.InfoSettingsAssetCreated)
                {
                    new LocHelpBox(Loc("MeshWeaver Settings saved to Assets/MeshWeaverSettings.asset."), MessageType.Warning).OnGUI();
                    _acknowledgeSettingsAssetCreated = true;
                }
            }
        }
    }
}