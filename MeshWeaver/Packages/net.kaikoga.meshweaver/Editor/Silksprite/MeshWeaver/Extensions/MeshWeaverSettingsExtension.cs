using Silksprite.MeshWeaver.Controllers;
using Silksprite.MeshWeaver.Utils;
using UnityEngine;

namespace Silksprite.MeshWeaver.Extensions
{
    public static class MeshWeaverSettingsExtension
    {
        public static Material DefaultMaterialOrDefault(this MeshWeaverSettings current)
        {
            var material = current.defaultMaterial;
            if (!material)
            {
                material = UnityAssetLocator.DefaultMaterial();
                current.defaultMaterial = material;
                MeshWeaverSettings.ApplySettings();
            }
            return material;

        }
    }
}