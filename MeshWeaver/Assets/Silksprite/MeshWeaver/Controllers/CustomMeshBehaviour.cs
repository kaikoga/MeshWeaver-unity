using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Models;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers
{
    [ExecuteAlways]
    public abstract class CustomMeshBehaviour : GeometryProvider
    {
        public LodMaskLayer lodMaskLayer = LodMaskLayer.LOD0;
        public bool autoUpdate;

        public MeshExportSettings.NormalGeneratorKind exportedNormalGeneratorKind;
        public float exportedNormalGeneratorAngle;
        
        public Material[] materials;

        public MeshFilter[] meshFilters;
        public MeshCollider[] meshColliders;

        Mesh _runtimeMesh;
        Mesh _runtimeColliderMesh;

        void Update()
        {
            if (_runtimeMesh == null || autoUpdate)
            {
                Compile();
            }
        }

        public void Compile()
        {
            if (_runtimeMesh == null) _runtimeMesh = new Mesh();
            OnPopulateMesh(lodMaskLayer, _runtimeMesh);

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
                OnPopulateMesh(LodMaskLayer.Collider, _runtimeColliderMesh);
                runtimeColliderMesh = _runtimeColliderMesh;
            }
            foreach (var meshCollider in meshColliders ?? Enumerable.Empty<MeshCollider>())
            {
                if (meshCollider) meshCollider.sharedMesh = runtimeColliderMesh;
            }
        }

        public void ExportMesh(LodMaskLayer lodMask, Mesh mesh)
        {
            OnPopulateMesh(lodMask, mesh);
        }

        protected virtual void OnPopulateMesh(LodMaskLayer lodMask, Mesh mesh)
        {
            mesh.Clear();
            var meshie = OnPopulateMesh(lodMask);
            meshie.ExportToMesh(mesh, new MeshExportSettings(exportedNormalGeneratorKind, exportedNormalGeneratorAngle));
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