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
            return material ? material : current.DefaultMaterial = UnityAssetLocator.DefaultMaterial();
        }
    }
}