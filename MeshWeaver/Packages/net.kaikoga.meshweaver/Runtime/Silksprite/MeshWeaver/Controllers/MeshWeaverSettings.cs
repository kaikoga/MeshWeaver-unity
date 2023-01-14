using System;
using Silksprite.MeshWeaver.Models;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers
{
    [ExcludeFromPreset]
    public class MeshWeaverSettings : ScriptableObject
    {
        static MeshWeaverSettingsData _current;

        public static MeshWeaverSettingsData Current => _current ?? (_current = new MeshWeaverSettingsData());

        [Serializable]
        public class MeshWeaverSettingsData
        {
            [SerializeField] LodMaskLayer currentLodMaskLayer = LodMaskLayer.LOD0;
            [SerializeField] string lang = "en";

            public LodMaskLayer CurrentLodMaskLayer
            {
                get => currentLodMaskLayer;
                set
                {
                    currentLodMaskLayer = value;
#if UNITY_EDITOR
                    if (!Application.isPlaying) UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
#endif
                }
            }

            public string Lang
            {
                get => lang;
                set => lang = value;
            }
        }

    }
}