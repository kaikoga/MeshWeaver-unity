using System.Collections.Generic;
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

        static Dictionary<string, string> _trCache;
        static Dictionary<string, GUIContent> _guiContentCache;

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
                _trCache = new Dictionary<string, string>();
                _guiContentCache = new Dictionary<string, GUIContent>();
            }

            if (_trCache.TryGetValue(key, out var tr)) return tr;
            var po = Po;
            tr = po != null ? po.GetLocalizedString(key) : key;
            _trCache.Add(key, tr);
            return tr;
        }
        
        public static GUIContent GUIContent(string key)
        {
            var tr = Tr(key); // Invalidate cache
            if (_guiContentCache.TryGetValue(key, out var guiContent)) return guiContent;
            guiContent = new GUIContent(tr, null, key);
            _guiContentCache.Add(key, guiContent);
            return guiContent;
        }
        
        public static LocalizedContent Loc(string key)
        {
            return new LocalizedContent(key);
        }
    }
}