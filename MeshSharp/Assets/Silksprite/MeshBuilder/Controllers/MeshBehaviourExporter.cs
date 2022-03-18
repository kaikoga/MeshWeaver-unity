using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers
{
    [RequireComponent(typeof(CustomMeshBehaviour))]
    public class MeshBehaviourExporter : MonoBehaviour
    {
        public Mesh outputMesh;
        public GameObject outputPrefab;

        public Mesh outputMeshLod1;
        public Mesh outputMeshLod2;
        public Mesh outputMeshForCollider;
    }
}