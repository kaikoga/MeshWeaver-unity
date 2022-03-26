using Silksprite.MeshBuilder.Models.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Meshes.Modifiers
{
    public class MeshRepeat : IMeshieModifier
    {
        readonly int _count;
        readonly Matrix4x4 _translation;

        public MeshRepeat(int count, Matrix4x4 translation)
        {
            _count = count;
            _translation = translation;
        }

        public Meshie Modify(Meshie meshie)
        {
            if (_count <= 1) return meshie;
            var builder = Meshie.Builder();
            var t = Matrix4x4.identity;
            for (var i = 0; i < _count; i++)
            {
                builder.Concat(meshie, t, 0);
                t *= _translation;
            }
            return builder.ToMeshie();
        }
    }
}