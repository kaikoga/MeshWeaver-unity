using System;
using Silksprite.MeshWeaver.Models;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers
{
    [CreateAssetMenu(fileName = "Mesh Behaviour Profile", menuName = "MeshWeaver/Mesh Behaviour Profile", order = 0)]
    public class MeshBehaviourProfile : ScriptableObject
    {
        public MeshBehaviourProfileData data;
    }

    [Serializable]
    public class MeshBehaviourProfileData
    {
        static MeshBehaviourProfileData _default;
        public static MeshBehaviourProfileData Default => _default = _default ?? new MeshBehaviourProfileData();

        public MeshExportSettings.NormalGeneratorKind realtimeNormalGeneratorKind = MeshExportSettings.NormalGeneratorKind.Default;
        public float realtimeNormalGeneratorAngle;
        public MeshExportSettings.LightmapGeneratorKind realtimeLightmapGeneratorKind = MeshExportSettings.LightmapGeneratorKind.None;

        public MeshExportSettings.NormalGeneratorKind exportedNormalGeneratorKind = MeshExportSettings.NormalGeneratorKind.Default;
        public float exportedNormalGeneratorAngle;
        public MeshExportSettings.LightmapGeneratorKind exportedLightmapGeneratorKind = MeshExportSettings.LightmapGeneratorKind.Default;

        public bool useLod;
        public bool useLod1;
        public bool useLod2;
        public bool useCollider = true;
        public bool useLightmap = true;
        [SerializeField] StaticEditorFlags staticEditorFlags;
        
#if UNITY_EDITOR
        public UnityEditor.StaticEditorFlags EditorStaticEditorFlags => (UnityEditor.StaticEditorFlags)staticEditorFlags;
#endif
        
        // NOTE: copy of UnityEditor.StaticEditorFlags
        [Flags]
        enum StaticEditorFlags
        {
            ContributeGI = 1,
            OccluderStatic = 2,
            OccludeeStatic = 16,
            BatchingStatic = 4,
            NavigationStatic = 8,
            OffMeshLinkGeneration = 32,
            ReflectionProbeStatic = 64,
        }
    }
}