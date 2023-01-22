using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Controllers;
using Silksprite.MeshWeaver.GUIActions.Extensions;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Scopes;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.GUIActions
{
    public class MainProviderHeader : GUIContainer
    {
        const float HeaderPopupWidth = 160f;
        const float HeaderLabelWidth = 100f;
        const float ExtendedLabelWidth = 150f;

        public MainProviderHeader(bool isExpanded, bool showFoldout, bool isExtended)
        {
            var banner = MeshWeaverAssetLocator.Banner();
            var infoSettingsAssetCreated = false;
            
            Add(Scoped(() => new BoxLayoutScope(MeshWeaverSkin.Primary), c =>
            {
                c.Add(Scoped(() => new LabelWidthScope(HeaderLabelWidth), () =>
                {
                    using (new LabelWidthScope(HeaderLabelWidth))
                    {
                        var height = isExpanded ? EditorGUIUtility.singleLineHeight * 3 + EditorGUIUtility.standardVerticalSpacing * 2 : EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
                        var rect = GUILayoutUtility.GetRect(0f, float.MaxValue, height, height);
                        var leftPane = rect;
                        leftPane.xMin += 10f;
                        leftPane.width -= HeaderPopupWidth + 10f;
                        var rightPane = rect;
                        rightPane.xMin = rect.xMax - HeaderPopupWidth;

                        var labelRect = leftPane;
                        labelRect.height = EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
                        GUI.DrawTexture(labelRect, banner, ScaleMode.ScaleToFit);

                        if (showFoldout)
                        {
                            var foldoutRect = labelRect;
                            foldoutRect.width = 0f;
                            isExpanded = EditorGUI.Foldout(foldoutRect, isExpanded, "");
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
                            var lod = EditorGUI.EnumPopup(popupRect, Loc("Current LOD").Tr, MeshWeaverSettings.Current.activeLodMaskLayer);
                            if (changed.changed)
                            {
                                MeshWeaverSettings.Current.activeLodMaskLayer = (LodMaskLayer)lod;
                                MeshWeaverSettings.ApplySettings();
                            }
                        }

                        if (isExpanded)
                        {
                            var versionRect = new Rect(leftPane.x, EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing * 2, leftPane.width, EditorGUIUtility.singleLineHeight);
                            GUI.Label(versionRect, $"MeshWeaver {MeshWeaverConstants.Version}");
                        }
                    }
                }));

                if (isExtended)
                {
                    c.Add(Scoped(() => new LabelWidthScope(ExtendedLabelWidth), c1 =>
                    {
                        c1.Add(Scoped(() => new EditorGUI.IndentLevelScope(1), co =>
                        {
                            var serializedObject = new SerializedObject(MeshWeaverSettings.Current);
                            co.Add(new LocPropertyField(serializedObject.FindProperty(nameof(MeshWeaverSettings.enableDumpGUI)), Loc("MeshWeaverSettings.enableDumpGUI")));
                            co.Add(new LocPropertyField(serializedObject.FindProperty(nameof(MeshWeaverSettings.enableAdvancedActions)), Loc("MeshWeaverSettings.enableAdvancedActions")));
                            co.Add(new LocPropertyField(serializedObject.FindProperty(nameof(MeshWeaverSettings.defaultMaterial)), Loc("MeshWeaverSettings.defaultMaterial")));
                            co.Add(new LocPropertyField(serializedObject.FindProperty(nameof(MeshWeaverSettings.profiles)), Loc("MeshWeaverSettings.profiles")));
                            co.Add(new LocButton(Loc("Reset to Default Profiles"), () =>
                            {
                                MeshWeaverSettings.Current.profiles = MeshWeaverAssetLocator.DefaultProfiles().ToArray();
                            }).WithEnabled(MeshWeaverSettings.Current.profiles.Length == 0));
                        }));
                    }));
                }

                if (MeshWeaverSettings.WarnMultipleSettingsAsset)
                {
                    c.Add(new LocHelpBox(Loc("Multiple MeshWeaver Settings asset found. Settings may or may not be saved."), MessageType.Warning));
                }
                c.Add(new LocHelpBox(Loc("MeshWeaver Settings saved to Assets/MeshWeaverSettings.asset."), MessageType.Warning)
                    .WithDisplay(() =>
                    {
                        infoSettingsAssetCreated |= MeshWeaverSettings.InfoSettingsAssetCreated;
                        MeshWeaverSettings.InfoSettingsAssetCreated = false;
                        return infoSettingsAssetCreated;
                    }));
            }));
        }
    }
}