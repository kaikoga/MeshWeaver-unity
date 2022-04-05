using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;

namespace Silksprite.MeshWeaver.Models.Meshes.Modifiers
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
            var result = Meshie.Builder();

            if (_front)
            {
                result.Vertices.AddRange(meshie.Vertices);
                result.Gons.AddRange(meshie.Gons);
            }
            if (_back)
            {
                var offset = result.Vertices.Count;
                result.Vertices.AddRange(meshie.Vertices);
                result.Gons.AddRange(meshie.Gons.Select(gon => gon.Reverse() + offset));
            }
            return result.ToMeshie();
        }
    }
}