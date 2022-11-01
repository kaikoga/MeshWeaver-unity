using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Utils
{
    public static class UnityAssetLocator
    {
        public static Material DefaultMaterial()
        {
            // Please don't break
            GlobalObjectId globalId;
            GlobalObjectId.TryParse("GlobalObjectId_V1-4-0000000000000000f000000000000000-10303-0", out globalId);
            return (Material)GlobalObjectId.GlobalObjectIdentifierToObjectSlow(globalId); 
        }
    }
}