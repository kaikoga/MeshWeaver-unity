using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers
{
    [DisallowMultipleComponent]
    public class TranslationProvider : MonoBehaviour
    {
        public Vector3 oneX = Vector3.right;
        public Vector3 oneY = Vector3.up;
        public Vector3 oneZ = Vector3.forward;
    }
}