using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
{
    public class PathReverseProvider : PathModifierProviderBase
    {
        protected override IPathieModifier CreateModifier() => new PathReverse();
    }
}