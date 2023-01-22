using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers;
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

        public static IEnumerable<MeshBehaviourProfile> DefaultProfiles()
        {
            yield return DefaultStaticLarge();
            yield return DefaultStaticMedium();
            yield return DefaultStaticSmall();
            yield return DefaultDynamic();
        }

        static MeshBehaviourProfile DefaultStaticLarge()
        {
            return AssetDatabase.LoadAssetAtPath<MeshBehaviourProfile>("Packages/net.kaikoga.meshweaver/Runtime/Default MeshBehaviour Profiles/Default-Static-Large-MeshProfile.asset");
        }

        static MeshBehaviourProfile DefaultStaticMedium()
        {
            return AssetDatabase.LoadAssetAtPath<MeshBehaviourProfile>("Packages/net.kaikoga.meshweaver/Runtime/Default MeshBehaviour Profiles/Default-Static-Medium-MeshProfile.asset");
        }

        static MeshBehaviourProfile DefaultStaticSmall()
        {
            return AssetDatabase.LoadAssetAtPath<MeshBehaviourProfile>("Packages/net.kaikoga.meshweaver/Runtime/Default MeshBehaviour Profiles/Default-Static-Small-MeshProfile.asset");
        }

        static MeshBehaviourProfile DefaultDynamic()
        {
            return AssetDatabase.LoadAssetAtPath<MeshBehaviourProfile>("Packages/net.kaikoga.meshweaver/Runtime/Default MeshBehaviour Profiles/Default-Dynamic-MeshProfile.asset");
        }
    }
}