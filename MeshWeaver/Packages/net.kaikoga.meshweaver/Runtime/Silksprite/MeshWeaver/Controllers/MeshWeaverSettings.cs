using System;
using System.Linq;
using Silksprite.MeshWeaver.Models;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers
{
    [ExcludeFromPreset]
    public class MeshWeaverSettings : ScriptableObject
    {
        static MeshWeaverSettings _settingsAsset;
        static MeshWeaverSettings SettingsAsset => _settingsAsset ? _settingsAsset : _settingsAsset = FindSettingsAsset();
        public static bool WarnMultipleSettingsAsset { get; private set; }
        public static bool InfoSettingsAssetCreated { get; set; }

        public static MeshWeaverSettingsData Current => SettingsAsset.data;

        [SerializeField] MeshWeaverSettingsData data = new MeshWeaverSettingsData();

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
            var settingsAsset = SettingsAsset;
            if (settingsAsset)
            {
                if (string.IsNullOrEmpty(UnityEditor.AssetDatabase.GetAssetPath(settingsAsset)))
                {
                    UnityEditor.AssetDatabase.CreateAsset(settingsAsset, "Assets/MeshWeaverSettings.asset");
                    UnityEditor.AssetDatabase.SaveAssets();
                    InfoSettingsAssetCreated = true;
                }
                UnityEditor.EditorUtility.SetDirty(settingsAsset);
            }
            if (!Application.isPlaying) UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
#endif
            _settingsAsset = null;
        }

        [Serializable]
        public class MeshWeaverSettingsData
        {
            [SerializeField] LodMaskLayer currentLodMaskLayer = LodMaskLayer.LOD0;
            [SerializeField] string lang = "en";

            [SerializeField] Material defaultMaterial;

            public LodMaskLayer CurrentLodMaskLayer
            {
                get => currentLodMaskLayer;
                set => currentLodMaskLayer = value;
            }

            public string Lang
            {
                get => lang;
                set => lang = value;
            }

            public Material DefaultMaterial
            {
                get => defaultMaterial;
                set => defaultMaterial = value;
            }
        }
    }
}