using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Paths.Modifiers
{
    public class PathRepeat : IPathieModifier
    {
        readonly int _count;
        readonly Matrix4x4 _translation;
        readonly bool _fromPath;
        readonly bool _smoothJoin;

        public PathRepeat(int count, Matrix4x4 translation, bool fromPath = false, bool smoothJoin = false)
        {
            _count = count;
            _translation = translation;
            _fromPath = fromPath;
            _smoothJoin = smoothJoin;
        }

        public PathRepeat(int count, bool fromPath, bool smoothJoin = false) : this(count, Matrix4x4.identity, fromPath, smoothJoin) { }

        public Pathie Modify(Pathie pathie)
        {
            if (_count <= 1) return pathie;
            var builder = Pathie.Builder(pathie.isLoop, _smoothJoin);
            var t = Matrix4x4.identity;
            var dt = _fromPath ? pathie.Diff.Translation : _translation; 
            for (var i = 0; i < _count; i++)
            {
                builder.Concat(pathie, t);
                t *= dt;
            }
            return builder.ToPathie();
        }
    }
}