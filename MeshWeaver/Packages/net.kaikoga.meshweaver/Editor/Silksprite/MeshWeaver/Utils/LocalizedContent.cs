using UnityEngine;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Utils
{
    public readonly struct LocalizedContent
    {
        readonly string _key;
        public LocalizedContent(string key) => _key = key;
        
        public string Id => $"mw-{_key.Replace(" ", "")}";
        public string Tr => Tr(_key);
        public GUIContent GUIContent => GUIContent(_key);
    }
}