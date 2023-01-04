using Silksprite.MeshWeaver.Controllers;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Utils
{
    public static class Localization
    {
        static string _lang;
        static LocalizationAsset _po; 
        static LocalizationAsset Po => _po ? _po : _po = AssetDatabase.LoadAssetAtPath<LocalizationAsset>(PoPath(_lang));

        static string PoPath(string lang)
        {
            return $"Packages/net.kaikoga.meshweaver/Editor/Silksprite/MeshWeaver/{lang}.po";
        }

        public static string Tr(string key)
        {
            if (_lang != MeshWeaverSettings.Current.lang)
            {
                _po = null;
                _lang = MeshWeaverSettings.Current.lang;
            }

            var po = Po;
            return po != null ? po.GetLocalizedString(key) : key;
        }
    }
}