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
        public bool updatesEveryFrame;

        [SerializeField] MeshBehaviourProfile profile;
        public MeshBehaviourProfileData ProfileData => profile ? profile.data : MeshBehaviourProfileData.Default;
        
        public bool overrideMaterials;
        public Material[] materials;

        LodMaskLayer _currentLodMaskLayer;
        Mesh _runtimeMesh;
        Mesh _runtimeColliderMesh;

        void Update()
        {
            if (_currentLodMaskLayer != MeshWeaverSettings.Current.activeLodMaskLayer)
            {
                _currentLodMaskLayer = MeshWeaverSettings.Current.activeLodMaskLayer;
                _runtimeMesh = null;
            }
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
            var lodMaskLayer = MeshWeaverSettings.Current.activeLodMaskLayer;

            if (_runtimeMesh == null) _runtimeMesh = new Mesh();
            OnPopulateMesh(lodMaskLayer, true, _runtimeMesh, !overrideMaterials, out materials);

            if (TryGetComponent<MeshFilter>(out var meshFilter))
            {
                meshFilter.sharedMesh = _runtimeMesh;
                if (materials != null && meshFilter.TryGetComponent<MeshRenderer>(out var meshRenderer))
                {
                    // NOTE: trim subMeshCount here because when the last submeshes have zero polys:
                    // - Excess Material is registered to materials array prior to mesh population, and 
                    // - _runtimeMesh does not have the submesh index because there is no Gon with the specific material index, so
                    // - materials.Length > _runtimeMesh.subMeshCount is possible and we can only know it here
                    meshRenderer.sharedMaterials = materials.Take(_runtimeMesh.subMeshCount).ToArray();
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
                OnPopulateMesh(LodMaskLayer.Collider, true, _runtimeColliderMesh, false, out _);
                runtimeColliderMesh = _runtimeColliderMesh;
            }
            if (TryGetComponent<MeshCollider>(out var meshCollider))
            {
                meshCollider.sharedMesh = runtimeColliderMesh;
            }
        }

        public void ExportMesh(LodMaskLayer lodMask, Mesh mesh, bool realtime)
        {
            OnPopulateMesh(lodMask, realtime, mesh, false, out _);
        }

        protected virtual void OnPopulateMesh(LodMaskLayer lodMask, bool realtime, Mesh mesh, bool collectMaterials, out Material[] outMaterials)
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

            outMaterials = collectMaterials ? meshie.Materials : null;
        }

        protected virtual Meshie OnPopulateMesh(LodMaskLayer lodMask)
        {
            return Meshie.Empty();
        }
    }
}