using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models
{
    public class Gon
    {
        readonly int[] _indices;
        public readonly Material material;

        public IReadOnlyCollection<int> Indices => _indices;

        public Gon(int[] indices, Material material)
        {
            _indices = indices;
            this.material = material;
        }

        public int this[int index] => _indices[index];

        public Gon Reverse() => new Gon(_indices.Reverse().ToArray(), material);

        public static Gon operator +(Gon gon, int offset)
        {
            // return new Gon(gon._indices.Select(i => i + offset).ToArray(), gon.MaterialIndex);

            var gonIndices = gon._indices;
            var gonSize = gonIndices.Length;
            var indices = new int[gonSize];
            for (var i = 0; i < gonSize; i++) indices[i] = gonIndices[i] + offset;
            return new Gon(indices, gon.material);

        }

        public override string ToString()
        {
            return $"{{{string.Join(", ", _indices)}}} [{material}]";
        }

    }
}