using System.Linq;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models.Base;

namespace Silksprite.MeshBuilder.Models.Meshes.Modifiers
{
    public class MeshReverse : IMeshieModifier
    {
        readonly bool _front;
        readonly bool _back;

        public MeshReverse(bool front, bool back)
        {
            _front = front;
            _back = back;
        }

        public static MeshReverse BackOnly => new MeshReverse(false, true);

        public Meshie Modify(Meshie meshie)
        {
            var result = Meshie.Empty();

            if (_front)
            {
                result.Vertices.AddRange(meshie.Vertices);
                result.Indices.AddRange(meshie.Indices);
            }
            if (_back)
            {
                var offset = result.Vertices.Count;
                result.Vertices.AddRange(meshie.Vertices);
                result.Indices.AddRange(meshie.Indices
                    .EachTrio((a, b, c) => new []{a, c, b})
                    .SelectMany(i => i)
                    .Select(i => i + offset));
            }
            return result;
        }
    }
}