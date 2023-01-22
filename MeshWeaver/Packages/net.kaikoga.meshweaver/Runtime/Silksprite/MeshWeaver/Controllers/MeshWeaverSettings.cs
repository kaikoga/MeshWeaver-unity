using System;
using System.Linq;
using Silksprite.MeshWeaver.Models;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers
{
    [ExcludeFromPreset]
    public class MeshWeaverSettings : ScriptableObject
    {
        [NonSerialized] public LodMaskLayer activeLodMaskLayer = LodMaskLayer.LOD0;

        public string lang = "en";

        public bool enableDumpGUI = true;
        public bool enableAdvancedActions = true;
        public Material defaultMaterial;
        public MeshBehaviourProfile[] profiles = {};

        public static bool WarnMultipleSettingsAsset { get; private set; }
        public static bool InfoSettingsAssetCreated { get; set; }

        static MeshWeaverSettings _current;
        public static MeshWeaverSettings Current => _current ? _current : _current = FindSettingsAsset();

        static MeshWeaverSettings FindSettingsAsset()
        {
#if UNITY_EDITOR
            var assets = UnityEditor.AssetDatabase.FindAssets($"t:{nameof(MeshWeaverSettings)}")
                .Select(guid => UnityEditor.AssetDatabase.LoadAssetAtPath<MeshWeaverSettings>(UnityEditor.AssetDatabase.GUIDToAssetPath(guid)))
                .ToArray();
#else
            var assets = Resources.FindObjectsOfTypeAll<MeshWeaverSettings>();
#endif
            WarnMultipleSettingsAsset = assets.Length > 1; 
            return assets.Length == 0 ? CreateInstance<MeshWeaverSettings>() : assets[0];
        }

        public static void ApplySettings()
        {
#if UNITY_EDITOR
            if (_current)
            {
                if (string.IsNullOrEmpty(UnityEditor.AssetDatabase.GetAssetPath(_current)))
                {
                    UnityEditor.AssetDatabase.CreateAsset(_current, "Assets/MeshWeaverSettings.asset");
                    UnityEditor.AssetDatabase.SaveAssets();
                    InfoSettingsAssetCreated = true;
                }
                UnityEditor.EditorUtility.SetDirty(_current);
            }
#endif
            _current = null;
        }
        
        
        public static void ApplyLodMaskLayer()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
#endif
        }
    }
}