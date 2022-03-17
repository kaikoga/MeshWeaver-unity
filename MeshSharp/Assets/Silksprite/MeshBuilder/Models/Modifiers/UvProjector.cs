using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Modifiers
{
    public class UvProjector : VertiesModifierBase
    {
        readonly Matrix4x4 _translation;
        readonly int _minIndex;

        public UvProjector(Matrix4x4 translation, int minIndex)
        {
            _translation = translation;
            _minIndex = minIndex;
        }

        protected override Vertie ModifyVertie(Vertie vertie)
        {
            var translation = _translation;
            return vertie.AddUv(new MuxLayer<Vector2>(translation.MultiplyPoint(vertie.Vertex), _minIndex));
        }
    }
}