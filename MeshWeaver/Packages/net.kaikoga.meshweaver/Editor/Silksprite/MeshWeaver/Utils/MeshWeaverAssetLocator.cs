using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Utils
{
    public static class MeshWeaverAssetLocator
    {
        public static Texture2D Banner()
        {
            return AssetDatabase.LoadAssetAtPath<Texture2D>("Packages/net.kaikoga.meshweaver/Editor/Images/banner.png");
        }
    }
}