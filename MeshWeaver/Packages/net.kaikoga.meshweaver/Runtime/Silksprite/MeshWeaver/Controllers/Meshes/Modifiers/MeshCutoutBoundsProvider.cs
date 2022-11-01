using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.CustomDrawers;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    public class MeshCutoutBoundsProvider : MeshModifierProviderBase
    {
        [SerializeField] [HideInInspector] bool hasLegacyBounds = true;
        [FormerlySerializedAs("bounds")]
        [SerializeField] [HideInInspector] Bounds legacyBounds;

        [BoundsCustom]
        public List<Bounds> boundsData = new List<Bounds>();
        
        public bool inside;
        [Range(0, 3)]
        public int numVertex = 1;

        protected override IMeshieModifier CreateModifier()
        {
            if (hasLegacyBounds)
            {
                boundsData = new List<Bounds> { legacyBounds };
                hasLegacyBounds = false;
            }
            return new MeshCutoutBounds(boundsData, inside, numVertex);
        }
    }
}