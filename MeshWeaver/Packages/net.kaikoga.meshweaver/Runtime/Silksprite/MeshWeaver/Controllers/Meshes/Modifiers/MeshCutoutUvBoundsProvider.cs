using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.CustomDrawers;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    public class MeshCutoutUvBoundsProvider : MeshModifierProviderBase
    {
        [SerializeField] [HideInInspector] bool hasLegacyUvArea = true;
        [FormerlySerializedAs("uvArea")]
        [SerializeField] [HideInInspector] Rect legacyUvArea = new Rect(0f, 0f, 1f, 1f);

        [RectCustom]
        public List<Rect> uvAreas = new List<Rect>{new Rect(0f, 0f, 1f, 1f)};
        
        public int uvChannel;
        public bool inside;
        [Range(0, 3)]
        public int numVertex = 1;

        protected override IMeshieModifier CreateModifier()
        {
            if (hasLegacyUvArea)
            {
                uvAreas = new List<Rect> { legacyUvArea };
                hasLegacyUvArea = false;
            }
            return new MeshCutoutUvBounds(uvAreas, uvChannel, inside, numVertex);
        }
    }
}