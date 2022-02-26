using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    public class GeometryProvider : MonoBehaviour
    {
        public Matrix4x4 Translation => transform.localToWorldMatrix; 
    }
}