using Silksprite.MeshBuilder.Models.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Paths.Modifiers
{
    public class PathRepeat : PathieModifier
    {
        readonly int _count;
        readonly Matrix4x4 _translation;
        readonly bool _fromPath;

        public PathRepeat(int count, Matrix4x4 translation, bool fromPath = false)
        {
            _count = count;
            _translation = translation;
            _fromPath = fromPath;
        }

        public PathRepeat(int count, bool fromPath) : this(count, Matrix4x4.identity, fromPath) { }

        public override Pathie Modify(Pathie pathie)
        {
            if (_count <= 1) return pathie;
            var result = new Pathie();
            var t = Matrix4x4.identity;
            var dt = _fromPath ? pathie.Diff.Translation : _translation; 
            for (var i = 0; i < _count; i++)
            {
                result.Concat(pathie, t);
                t *= dt;
            }
            return result;
        }
    }
}