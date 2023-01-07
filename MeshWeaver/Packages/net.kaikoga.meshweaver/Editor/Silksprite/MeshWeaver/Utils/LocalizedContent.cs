using UnityEngine;

namespace Silksprite.MeshWeaver.Utils
{
    public readonly struct LocalizedContent
    {
        readonly string _key;
        public LocalizedContent(string key) => _key = key;
        
        public string Tr => Localization.Tr(_key);
        public GUIContent GUIContent => Localization.GUIContent(_key);
    }
}