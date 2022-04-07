using Silksprite.MeshWeaver.Models;
using UnityEditor;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public class GeometryProviderEditor : Editor
    {
        LodMaskLayer? _lodMaskLayer;

        protected LodMaskLayer GuessCurrentLodMaskLayer()
        {
            if (!_lodMaskLayer.HasValue)
            {
                var geometryProvider = (GeometryProvider)target;
                var meshBehaviour = geometryProvider.GetComponentInParent<CustomMeshBehaviour>();
                _lodMaskLayer = meshBehaviour != null ? meshBehaviour.lodMaskLayer : LodMaskLayer.LOD0;
            }

            return _lodMaskLayer.GetValueOrDefault();
        }
    }
}