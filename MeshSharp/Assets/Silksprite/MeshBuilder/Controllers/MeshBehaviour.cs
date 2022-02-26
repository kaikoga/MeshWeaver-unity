using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers
{
    [ExecuteAlways]
    public class MeshBehaviour : MonoBehaviour
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
            sharedMesh = new Mesh();
            OnPopulateMesh(sharedMesh);

            foreach (var meshFilter in meshFilters)
            {
                meshFilter.sharedMesh = sharedMesh;
            }
        }
        
        protected virtual void OnPopulateMesh(Mesh mesh)
        {
            mesh.subMeshCount = 1;
            mesh.SetVertices(new []
            {
                Vector3.zero,
                Vector3.right,
                Vector3.up
            });
            mesh.SetTriangles(new[] { 0, 1, 2 }, 0);
        }
    }
}