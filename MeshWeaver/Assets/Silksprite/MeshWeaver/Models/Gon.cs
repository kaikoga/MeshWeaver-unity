using System.Collections.Generic;
using System.Linq;

namespace Silksprite.MeshWeaver.Models
{
    public class Gon
    {
        readonly int[] _indices;
        public readonly int MaterialIndex;

        public IReadOnlyCollection<int> Indices => _indices;

        public Gon(int[] indices, int materialIndex)
        {
            _indices = indices;
            MaterialIndex = materialIndex;
        }

        public int this[int index] => _indices[index];

        public Gon Reverse() => new Gon(_indices.Reverse().ToArray(), MaterialIndex);

        public static Gon operator +(Gon gon, int offset)
        {
            // return new Gon(gon._indices.Select(i => i + offset).ToArray(), gon.MaterialIndex);

            var gonIndices = gon._indices;
            var gonSize = gonIndices.Length;
            var indices = new int[gonSize];
            for (var i = 0; i < gonSize; i++) indices[i] = gonIndices[i] + offset;
            return new Gon(indices, gon.MaterialIndex);

        }

        public override string ToString()
        {
            return $"{{{string.Join(", ", _indices)}}} [{MaterialIndex}]";
        }

    }
}