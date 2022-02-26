using Silksprite.MeshBuilder.Models;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers
{
    [ExecuteAlways]
    public abstract class CustomMeshBehaviour : MonoBehaviour
    {
        public Mesh sharedMesh;

        public MeshFilter[] meshFilters;

        void Update()
        {
            if (sharedMesh == null)
            {
                Compile();
            }
        }

        public void Compile()
        {
            if (sharedMesh == null) sharedMesh = new Mesh();
            OnPopulateMesh(sharedMesh);

            if (meshFilters == null) return;

            foreach (var meshFilter in meshFilters)
            {
                meshFilter.sharedMesh = sharedMesh;
            }
        }

        protected virtual void OnPopulateMesh(Mesh mesh)
        {
            mesh.Clear();
            var meshie = new Meshie();
            OnPopulateMesh(meshie);
            meshie.ExportToMesh(mesh);
        }

        protected virtual void OnPopulateMesh(Meshie meshie)
        {
        }
    }
}