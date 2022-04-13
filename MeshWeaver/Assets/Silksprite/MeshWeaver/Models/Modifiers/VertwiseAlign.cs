using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Modifiers
{
    public class VertwiseAlign : VertwiseModifierBase
    {
        readonly bool _alignPosition;
        readonly bool _alignRotation;
        readonly bool _alignScale;

        public VertwiseAlign(bool alignPosition, bool alignRotation, bool alignScale)
        {
            _alignPosition = alignPosition;
            _alignRotation = alignRotation;
            _alignScale = alignScale;
        }

        protected override IEnumerable<Vertie> Modify(IEnumerable<Vertie> vertices)
        {
            var first = vertices.FirstOrDefault();
            if (first == null) return vertices;

            var m = first.Translation;
            var firstPosition = new Vector3(m.m03, m.m13, m.m23);
            var firstRotation = m.rotation;
            var firstScale = m.lossyScale;
            
            Vertie SimpleModifier(Vertie vertie)
            {
                var t = m;
                var v = vertie.Vertex;
                t.m03 = v.x;
                t.m13 = v.y;
                t.m23 = v.z;
                return vertie.WithTranslation(t);
            }
            Vertie Modifier(Vertie vertie)
            {
                var t = vertie.Translation;
                var position = new Vector3(t.m03, t.m13, t.m23);
                var rotation = t.rotation;
                var scale = t.lossyScale;
                return vertie.WithTranslation(Matrix4x4.TRS(_alignPosition ? firstPosition : position,
                    _alignRotation ? firstRotation : rotation,
                    _alignScale ? firstScale : scale));
            }

            var modifier = _alignRotation && _alignScale && !_alignPosition ? (Func<Vertie, Vertie>)SimpleModifier : Modifier;
            return vertices.Select(modifier);
        }
    }
}