using Silksprite.MeshBuilder.Models;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers
{
    public class MeshProvider : MonoBehaviour
    {
        public Meshie ToMeshie()
        {
            var meshie = new Meshie();
            meshie.Vertices.AddRange(new []
            {
                Vector3.zero,
                Vector3.up,
                Vector3.right
                
            });
            meshie.Indices.AddRange(new []
            {
                0,
                1,
                2
            });
            return meshie;
        }
    }
}