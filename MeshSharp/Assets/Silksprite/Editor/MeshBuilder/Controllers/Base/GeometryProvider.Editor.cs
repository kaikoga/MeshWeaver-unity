using Silksprite.MeshBuilder.Models;
using UnityEditor;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    public class GeometryProviderEditor : Editor
    {
        protected LodMaskLayer GuessCurrentLodMaskLayer()
        {
            var geometryProvider = (GeometryProvider)target;
            var meshBehaviour = geometryProvider.GetComponentInParent<CustomMeshBehaviour>();
            return meshBehaviour != null ? meshBehaviour.lodMaskLayer : LodMaskLayer.LOD0;
        }
    }
}