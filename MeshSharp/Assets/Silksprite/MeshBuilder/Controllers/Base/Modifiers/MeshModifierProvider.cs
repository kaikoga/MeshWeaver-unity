using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Base.Modifiers
{
    public abstract class MeshModifierProvider : MonoBehaviour
    {
        public LodMask lodMask = LodMask.All;

        // ReSharper disable once Unity.RedundantEventFunction
        void Start()
        {
            // The sole reason for this empty method is for showing enabled checkbox
        }

        public abstract MeshieModifier Modifier { get; }
    }
}