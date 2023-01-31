using Silksprite.MeshWeaver.Controllers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers
{
    [RequireComponent(typeof(MeshBehaviourBase))]
    public class MeshBehaviourExporter : MonoBehaviour
    {
        public bool overrideMaterials;
        public Material[] materials;

        public Mesh outputMesh;
        public GameObject outputPrefab;

        public Mesh outputMeshLod1;
        public Mesh outputMeshLod2;
        public Mesh outputMeshForCollider;
    }
}