using Silksprite.MeshBuilder.Models.Extensions;
using Silksprite.MeshBuilder.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Modifiers
{
    public class UvProjector : VertwiseModifierBase
    {
        readonly Matrix4x4 _translation;
        readonly int _uvChannel;

        public UvProjector(Matrix4x4 translation, int uvChannel)
        {
            _translation = translation;
            _uvChannel = uvChannel;
        }

        protected override Vertie Modify(Vertie vertie)
        {
            var translation = _translation;
            return vertie.AddUv(translation.MultiplyPoint(vertie.Vertex), _uvChannel);
        }
    }
}