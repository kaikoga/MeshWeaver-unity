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
        
        public Mesh sharedMesh;
        public Mesh outputMesh;

        public MeshFilter[] meshFilters;
        public MeshCollider[] meshColliders;

        void Update()
        {
            if (sharedMesh == null || autoUpdate)
            {
                Compile();
            }
        }

        public void Compile()
        {
            if (sharedMesh == null) sharedMesh = new Mesh();
            OnPopulateMesh(sharedMesh);

            foreach (var meshFilter in meshFilters ?? Enumerable.Empty<MeshFilter>())
            {
                if (meshFilter) meshFilter.sharedMesh = sharedMesh;
            }
            foreach (var meshCollider in meshColliders ?? Enumerable.Empty<MeshCollider>())
            {
                if (meshCollider) meshCollider.sharedMesh = sharedMesh;
            }
        }

        public Mesh ExportMesh()
        {
            var mesh = new Mesh();
            OnPopulateMesh(mesh);
            return mesh;
        }

        protected virtual void OnPopulateMesh(Mesh mesh)
        {
            mesh.Clear();
            var meshie = new Meshie();
            OnPopulateMesh((LodMask)lodMaskLayer, meshie);
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