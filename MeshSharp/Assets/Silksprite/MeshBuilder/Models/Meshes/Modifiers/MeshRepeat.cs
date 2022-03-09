using Silksprite.MeshBuilder.Models.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Meshes.Modifiers
{
    public class MeshRepeat : MeshModifier
    {
        readonly int _count;
        readonly Matrix4x4 _translation;

        public MeshRepeat(int count, Matrix4x4 translation)
        {
            _count = count;
            _translation = translation;
        }

        public override Meshie Modify(Meshie meshie)
        {
            if (_count <= 1) return meshie;
            var result = new Meshie();
            var t = Matrix4x4.identity;
            for (var i = 0; i < _count; i++)
            {
                result.Concat(meshie, t);
                t *= _translation;
            }
            return result;
        }
    }
}