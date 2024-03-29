using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
{
    public class PathBevelProvider : PathModifierProviderBase
    {
        [Min(1)]
        public int subdivision = 3;
        [Range(0f, 1f)]
        public float size = 0.5f;
        [Range(-1f, 1f)]
        public float strength = 0.5f;

        protected override IPathieModifier CreateModifier() => new PathBevel(subdivision, size, strength);
    }
}