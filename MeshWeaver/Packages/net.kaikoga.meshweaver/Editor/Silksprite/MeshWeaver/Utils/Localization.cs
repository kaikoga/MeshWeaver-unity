using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Utils
{
    public static class Localization
    {
        static string PoPath(string lang)
        {
            return $"Packages/net.kaikoga.meshweaver/Editor/Silksprite/MeshWeaver/{lang}.po";
        }

        static readonly LocalizationCache<LocalizationAsset> PoCache = new LocalizationCache<LocalizationAsset>();
        static LocalizationAsset Po => PoCache.FindOrCreate("po", (lang, key) => AssetDatabase.LoadAssetAtPath<LocalizationAsset>(PoPath(lang)));

        static readonly LocalizationCache<string> TrCache = new LocalizationCache<string>();
        static readonly LocalizationCache<GUIContent> GUIContentCache = new LocalizationCache<GUIContent>();

        public static string Tr(string key) => TrCache.FindOrCreate(key, (l, k) => Po != null ? Po.GetLocalizedString(k) : k);

        public static GUIContent GUIContent(string key) => GUIContentCache.FindOrCreate(key, (l, k) => new GUIContent(Po.GetLocalizedString(k), null, k));

        public static LocalizedContent Loc(string key) => new LocalizedContent(key);

        class LocalizationCache<T>
        {
            string _lang;
            Dictionary<string, T> _cache = new Dictionary<string, T>();

            public delegate T Generator(string lang, string key); 

            public T FindOrCreate(string key, Generator generator)
            {
                if (_lang != MeshWeaverSettings.Current.lang)
                {
                    _lang = MeshWeaverSettings.Current.lang;
                    _cache = new Dictionary<string, T>();
                }
                if (_cache.TryGetValue(key, out var value)) return value;
                value = generator(_lang, key);
                _cache.Add(key, value);
                return value;
            }
        }
    }
}