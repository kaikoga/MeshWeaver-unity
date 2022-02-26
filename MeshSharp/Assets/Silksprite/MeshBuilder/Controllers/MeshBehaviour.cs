using System.Linq;
using Silksprite.MeshBuilder.Models;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers
{
    [ExecuteAlways]
    public class MeshBehaviour : MonoBehaviour
    {
        public Mesh sharedMesh;

        public MeshProvider[] meshProviders;

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
            foreach (var meshProvider in meshProviders.Where(component => component.isActiveAndEnabled))
            {
                meshie.Concat(meshProvider.ToMeshie());
            }
            meshie.ExportToMesh(mesh);
        }
    }
}