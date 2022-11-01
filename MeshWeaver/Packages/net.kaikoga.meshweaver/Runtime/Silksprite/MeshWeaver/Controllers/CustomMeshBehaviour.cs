using System.Linq;
using Silksprite.MeshWeaver.Models;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Silksprite.MeshWeaver.Controllers
{
    [ExecuteAlways]
    public abstract class CustomMeshBehaviour : MonoBehaviour
    {
        public LodMaskLayer lodMaskLayer = LodMaskLayer.LOD0;
        public bool updatesEveryFrame;

        [SerializeField] MeshBehaviourProfile profile;
        public MeshBehaviourProfileData ProfileData => profile ? profile.data : MeshBehaviourProfileData.Default;
        
        public Material[] materials;

        public MeshFilter[] meshFilters;
        public MeshCollider[] meshColliders;

        Mesh _runtimeMesh;
        Mesh _runtimeColliderMesh;

        void Update()
        {
            if (_runtimeMesh == null || updatesEveryFrame)
            {
                Compile();
            }
#if UNITY_EDITOR
            else
            {
                var activeSelection = Selection.activeTransform; 
                if (activeSelection && activeSelection.IsChildOf(transform)) Compile();
            }
#endif
        }

        public void Compile()
        {
            if (_runtimeMesh == null) _runtimeMesh = new Mesh();
            OnPopulateMesh(lodMaskLayer, _runtimeMesh, true);

            var sharedMaterials = (materials?.Length ?? 0) > 0 ? materials : null;

            foreach (var meshFilter in meshFilters?.Where(m => m != null) ?? Enumerable.Empty<MeshFilter>())
            {
                if (meshFilter) meshFilter.sharedMesh = _runtimeMesh;
                if (sharedMaterials != null)
                {
                    if (meshFilter.TryGetComponent<MeshRenderer>(out var meshRenderer)) meshRenderer.sharedMaterials = sharedMaterials;
                }
            }

            Mesh runtimeColliderMesh;
            if (lodMaskLayer == LodMaskLayer.Collider)
            {
                runtimeColliderMesh = _runtimeMesh;
            }
            else
            {
                if (_runtimeColliderMesh == null) _runtimeColliderMesh = new Mesh();
                OnPopulateMesh(LodMaskLayer.Collider, _runtimeColliderMesh, true);
                runtimeColliderMesh = _runtimeColliderMesh;
            }
            foreach (var meshCollider in meshColliders ?? Enumerable.Empty<MeshCollider>())
            {
                if (meshCollider) meshCollider.sharedMesh = runtimeColliderMesh;
            }
        }

        public void ExportMesh(LodMaskLayer lodMask, Mesh mesh, bool realtime)
        {
            OnPopulateMesh(lodMask, mesh, realtime);
        }

        protected virtual void OnPopulateMesh(LodMaskLayer lodMask, Mesh mesh, bool realtime)
        {
            mesh.Clear();
            var profileData = ProfileData;
            var meshie = OnPopulateMesh(lodMask);
            var forCollider = lodMask == LodMaskLayer.Collider;
            if (realtime)
            {
                meshie.ExportToMesh(mesh, new MeshExportSettings(
                    forCollider ? MeshExportSettings.NormalGeneratorKind.None : profileData.realtimeNormalGeneratorKind,
                    profileData.realtimeNormalGeneratorAngle,
                    forCollider ? MeshExportSettings.LightmapGeneratorKind.None : profileData.realtimeLightmapGeneratorKind));
            }
            else
            {
                meshie.ExportToMesh(mesh, new MeshExportSettings(
                    forCollider ? MeshExportSettings.NormalGeneratorKind.None : profileData.exportedNormalGeneratorKind,
                    profileData.exportedNormalGeneratorAngle,
                    forCollider ? MeshExportSettings.LightmapGeneratorKind.None : profileData.exportedLightmapGeneratorKind));
            }
        }

        protected virtual Meshie OnPopulateMesh(LodMaskLayer lodMask)
        {
            return Meshie.Empty();
        }

        public void CollectMaterials()
        {
            OnCollectMaterials();
            Compile();
        }

        protected virtual void OnCollectMaterials() { }
    }
}