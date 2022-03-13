using System.Collections.Generic;
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
        
        public Mesh outputMesh;

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
            var lodMask = (LodMask)lodMaskLayer;

            if (_runtimeMesh == null) _runtimeMesh = new Mesh();
            OnPopulateMesh(lodMask, _runtimeMesh);

            foreach (var meshFilter in meshFilters ?? Enumerable.Empty<MeshFilter>())
            {
                if (meshFilter) meshFilter.sharedMesh = _runtimeMesh;
            }

            Mesh runtimeColliderMesh;
            if (lodMask == LodMask.Collider)
            {
                runtimeColliderMesh = _runtimeMesh;
            }
            else
            {
                if (_runtimeColliderMesh == null) _runtimeColliderMesh = new Mesh();
                OnPopulateMesh(LodMask.Collider, _runtimeColliderMesh);
                runtimeColliderMesh = _runtimeColliderMesh;
            }
            foreach (var meshCollider in meshColliders ?? Enumerable.Empty<MeshCollider>())
            {
                if (meshCollider) meshCollider.sharedMesh = runtimeColliderMesh;
            }
        }

        public Mesh ExportMesh(LodMask lodMask)
        {
            var mesh = new Mesh();
            OnPopulateMesh(lodMask, mesh);
            return mesh;
        }

        protected virtual void OnPopulateMesh(LodMask lodMask, Mesh mesh)
        {
            mesh.Clear();
            var meshie = new Meshie();
            OnPopulateMesh(lodMask, meshie);
            meshie.ExportToMesh(mesh);
        }

        protected virtual void OnPopulateMesh(LodMask lodMask, Meshie meshie)
        {
        }

        protected static void CollectMeshies(IEnumerable<MeshProvider> meshProviders, LodMask lod, Meshie meshie)
        {
            foreach (var meshProvider in meshProviders.Where(c => c != null && c.gameObject.activeSelf))
            {
                meshie.Concat(meshProvider.ToMeshie(lod), meshProvider.Translation);
            }
        }
    }
}