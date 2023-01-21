using Silksprite.MeshWeaver.Controllers;
using Silksprite.MeshWeaver.Utils;
using UnityEngine;

namespace Silksprite.MeshWeaver.Extensions
{
    public static class MeshWeaverSettingsExtension
    {
        public static Material DefaultMaterialOrDefault(this MeshWeaverSettings.MeshWeaverSettingsData current)
        {
            var material = current.DefaultMaterial;
            if (!material)
            {
                material = UnityAssetLocator.DefaultMaterial();
                current.DefaultMaterial = material;
                MeshWeaverSettings.ApplySettings();
            }
            return material;

        }
    }
}