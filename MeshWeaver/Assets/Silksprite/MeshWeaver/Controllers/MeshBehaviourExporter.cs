using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers
{
    [RequireComponent(typeof(CustomMeshBehaviour))]
    public class MeshBehaviourExporter : MonoBehaviour
    {
        public Material[] materials;

        public Mesh outputMesh;
        public GameObject outputPrefab;

        public bool useLod;
        public bool useLod1;
        public bool useLod2;
        public bool useCollider = true;
        
        public Mesh outputMeshLod1;
        public Mesh outputMeshLod2;
        public Mesh outputMeshForCollider;
    }
}