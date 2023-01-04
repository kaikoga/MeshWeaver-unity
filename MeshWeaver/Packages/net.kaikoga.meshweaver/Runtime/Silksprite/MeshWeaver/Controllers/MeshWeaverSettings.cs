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
            public LodMaskLayer currentLodMaskLayer = LodMaskLayer.LOD0;
            public string lang = "en";
        }
    }
}