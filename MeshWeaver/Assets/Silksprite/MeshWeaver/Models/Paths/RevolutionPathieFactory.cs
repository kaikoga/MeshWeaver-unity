using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Paths
{
    public class RevolutionPathieFactory : IPathieFactory
    {
        readonly float _min;
        readonly float _max;
        readonly Vector3 _vector;
        readonly int _subdivision;
        readonly Axis _axis;
        readonly bool _isLoop;
        readonly bool _fullCircle;

        public RevolutionPathieFactory(float min, float max, Vector3 vector, int subdivision, Axis axis, bool isLoop)
        {
            _min = min;
            _max = max;
            _vector = vector;
            _subdivision = subdivision;
            _axis = axis;
            _isLoop = isLoop;
            _fullCircle = _isLoop && (_max - _min) % 360f == 0f;
        }

        public Pathie Build(LodMaskLayer lod)
        {
            var degs = Enumerable.Range(0, _subdivision + (_fullCircle ? 0 : 1))
                .Select(i => _min + (_max - _min) * i / _subdivision);
            IEnumerable<Matrix4x4> mm;

            switch (_axis)
            {
                case Axis.X:
                    mm = degs.Select(deg => Matrix4x4.Rotate(Quaternion.Euler(deg, 0, 0)) * Matrix4x4.Translate(_vector));
                    break;
                case Axis.Y:
                    mm = degs.Select(deg => Matrix4x4.Rotate(Quaternion.Euler(0, deg, 0)) * Matrix4x4.Translate(_vector));
                    break;
                case Axis.Z:
                    mm = degs.Select(deg => Matrix4x4.Rotate(Quaternion.Euler(0, 0, deg)) * Matrix4x4.Translate(_vector));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var vertices = mm.Select(m => new Vertie(m));
            return new Pathie(vertices, _isLoop);
        }
        
        
        public enum Axis
        {
            X,
            Y,
            Z
        }

    }
}