using System;
using Silksprite.MeshWeaver.Models;
using UnityEditor;
using static Silksprite.MeshWeaver.Utils.Localization;
using Object = UnityEngine.Object;

namespace Silksprite.MeshWeaver.Controllers.Utils
{
    public static class MeshWeaverControllerGUI
    {
        static readonly string[] langs = { "en", "ja" };
        
        public static void LodSelectorGUI(Object target)
        {
            using (var changedScope = new EditorGUI.ChangeCheckScope())
            {
                var newLang = EditorGUILayout.Popup(Tr("Language"), Array.IndexOf(langs, MeshWeaverSettings.Current.lang), langs);
                if (newLang >= 0) MeshWeaverSettings.Current.lang = langs[newLang]; 
                MeshWeaverSettings.Current.currentLodMaskLayer = (LodMaskLayer)EditorGUILayout.EnumPopup(Tr("Current LOD (Global)"), MeshWeaverSettings.Current.currentLodMaskLayer);
                if (changedScope.changed)
                {
                    EditorUtility.SetDirty(target);
                }
            }
        }
    }
}