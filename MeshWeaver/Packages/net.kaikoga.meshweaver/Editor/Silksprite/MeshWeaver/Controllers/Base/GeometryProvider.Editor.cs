using Silksprite.MeshWeaver.Models;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class GeometryProviderEditor : Editor
    {
        LodMaskLayer? _lodMaskLayer;

        // FIXME: I think this should be replaced by global editingLodMaskLayer and renderLodMaskLayer 
        protected LodMaskLayer GuessCurrentLodMaskLayer()
        {
            if (!_lodMaskLayer.HasValue)
            {
                var geometryProvider = (MonoBehaviour)target;
                var meshBehaviour = geometryProvider.GetComponentInParent<CustomMeshBehaviour>();
                _lodMaskLayer = meshBehaviour != null ? meshBehaviour.lodMaskLayer : LodMaskLayer.LOD0;
            }

            return _lodMaskLayer.GetValueOrDefault();
        }
    }
}