using System.Linq;
using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers
{
    [ExecuteAlways]
    public abstract class CustomMeshBehaviour : GeometryProvider
    {
        public LodMaskLayer lodMaskLayer = LodMaskLayer.LOD0;
        public bool autoUpdate;
        
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

            foreach (var meshFilter in meshFilters ?? Enumerable.Empty<MeshFilter>())
            {
                if (meshFilter) meshFilter.sharedMesh = _runtimeMesh;
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
            var meshie = new Meshie();
            OnPopulateMesh(lodMask, meshie);
            meshie.ExportToMesh(mesh);
        }

        protected virtual void OnPopulateMesh(LodMaskLayer lodMask, Meshie meshie)
        {
        }
    }
}