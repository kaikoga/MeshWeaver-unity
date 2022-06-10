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
        readonly float _radius;
        readonly int _steps;
        readonly Axis _axis;
        readonly bool _isLoop;

        public RevolutionPathieFactory(float min, float max, float radius, int steps, Axis axis, bool isLoop)
        {
            _min = min;
            _max = max;
            _radius = radius;
            _steps = steps;
            _axis = axis;
            _isLoop = isLoop;
        }

        public Pathie Build(LodMaskLayer lod)
        {
            var drs = Enumerable.Range(0, _steps)
                .Select(i => _min + (_max - _min) * i / (_steps - (_isLoop ? 0 : 1)))
                .Select(deg => new { deg, rad = deg * Mathf.Deg2Rad });
            IEnumerable<Matrix4x4> mm;
            
            Matrix4x4 TRS(Vector3 pos, Quaternion q)
            {
                return Matrix4x4.Translate(pos) * Matrix4x4.Rotate(q);
            }

            switch (_axis)
            {
                case Axis.X:
                    mm = drs.Select(dr =>
                    {
                        var deg = dr.deg;
                        var rad = dr.rad;
                        return TRS(new Vector3(0, Mathf.Cos(rad), Mathf.Sin(rad)) * _radius, Quaternion.Euler(deg, 0, 0));
                    });
                    break;
                case Axis.Y:
                    mm = drs.Select(dr =>
                    {
                        var deg = dr.deg;
                        var rad = dr.rad;
                        return TRS(new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad)) * _radius, Quaternion.Euler(0, deg, 0));
                    });
                    break;
                case Axis.Z:
                    mm = drs.Select(dr =>
                    {
                        var deg = dr.deg;
                        var rad = dr.rad;
                        return TRS(new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * _radius, Quaternion.Euler(0, 0, deg));
                    });
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