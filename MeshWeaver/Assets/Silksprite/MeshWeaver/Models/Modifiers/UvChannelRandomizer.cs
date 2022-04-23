using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;
using Random = System.Random;

namespace Silksprite.MeshWeaver.Models.Modifiers
{
    public class UvChannelRandomizer : VertwiseModifierBase
    {
        readonly Vector2 _base;
        readonly Vector2 _range;
        readonly int _seed;
        readonly int _uvChannel;

        public UvChannelRandomizer(Vector2 @base, Vector2 range, int seed, int uvChannel)
        {
            _base = @base;
            _range = range;
            _seed = seed;
            _uvChannel = uvChannel;
        }

        protected override IEnumerable<Vertie> Modify(IEnumerable<Vertie> vertices)
        {
            var random = new Random(_seed);
            foreach (var vertie in vertices) yield return vertie.WithUvs(
                vertie.Uvs.WithMuxChannelValue(_uvChannel, uv => uv + _base + new Vector2(_range.x * (float)random.NextDouble(), _range.y * (float)random.NextDouble())));
        }
    }
}