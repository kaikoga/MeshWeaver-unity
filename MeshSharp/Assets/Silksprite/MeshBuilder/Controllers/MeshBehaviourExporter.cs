using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers
{
    [RequireComponent(typeof(CustomMeshBehaviour))]
    public class MeshBehaviourExporter : MonoBehaviour
    {
        public Mesh outputMesh;
        public GameObject outputPrefab;
    }
}