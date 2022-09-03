using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers
{
    [RequireComponent(typeof(CustomMeshBehaviour))]
    public class MeshBehaviourExporter : MonoBehaviour
    {
        public bool overrideMaterials;
        public Material[] materials;

        public Mesh outputMesh;
        public GameObject outputPrefab;

        [SerializeField] MeshBehaviourProfile profile;
        public MeshBehaviourProfileData ProfileData => profile ? profile.data : MeshBehaviourProfileData.Default;

        public Mesh outputMeshLod1;
        public Mesh outputMeshLod2;
        public Mesh outputMeshForCollider;
    }
}