using System.Collections.Generic;
using Silksprite.MeshWeaver.Models.Extensions;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;
using Random = System.Random;

namespace Silksprite.MeshWeaver.Models.Modifiers
{
    public class UvRandomizer : VertwiseModifierBase
    {
        readonly Vector2 _base;
        readonly Vector2 _range;
        readonly int _seed;

        public UvRandomizer(Vector2 @base, Vector2 range, int seed)
        {
            _base = @base;
            _range = range;
            _seed = seed;
        }

        protected override IEnumerable<Vertie> Modify(IEnumerable<Vertie> vertices)
        {
            var random = new Random(_seed);
            foreach (var vertie in vertices) yield return vertie.WithUvs(
                vertie.Uvs.SelectMuxValues(uv => uv + _base + new Vector2(_range.x * (float)random.NextDouble(), _range.y * (float)random.NextDouble())));
        }
    }
}