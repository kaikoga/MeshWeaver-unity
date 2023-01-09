using System;
using Silksprite.MeshWeaver.Models;
using UnityEditor;
using static Silksprite.MeshWeaver.Utils.Localization;

namespace Silksprite.MeshWeaver.Controllers.Utils
{
    public static class MeshWeaverControllerGUILayout
    {
        static readonly string[] langs = { "en", "ja" };

        public static void LangSelectorGUI()
        {
            using (var changedScope = new EditorGUI.ChangeCheckScope())
            {
                var newLang = EditorGUILayout.Popup(Tr("Language (Global)"), Array.IndexOf(langs, MeshWeaverSettings.Current.lang), langs);
                if (newLang >= 0) MeshWeaverSettings.Current.lang = langs[newLang]; 
                if (changedScope.changed)
                {
                    EditorApplication.QueuePlayerLoopUpdate();
                }
            }
        }

        public static void LodSelectorGUI()
        {
            using (var changedScope = new EditorGUI.ChangeCheckScope())
            {
                MeshWeaverSettings.Current.currentLodMaskLayer = (LodMaskLayer)EditorGUILayout.EnumPopup(Tr("Current LOD (Global)"), MeshWeaverSettings.Current.currentLodMaskLayer);
                if (changedScope.changed)
                {
                    EditorApplication.QueuePlayerLoopUpdate();
                }
            }
        }
    }
}