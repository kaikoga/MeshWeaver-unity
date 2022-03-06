using Silksprite.MeshBuilder.Models.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Paths.Modifiers
{
    public class UvGenerator : PathModifier
    {
        readonly Vector2 _min;
        readonly Vector2 _max;

        public UvGenerator(Vector2 min, Vector2 max)
        {
            _min = min;
            _max = max;
        }

        public override Pathie Modify(Pathie pathie)
        {
            var iMax = pathie.Vertices.Count - 1;
            return pathie.Modify((vertie, i) => new Vertie(vertie.Translation, _min + (_max - _min) * i / iMax));
        }
    }
}