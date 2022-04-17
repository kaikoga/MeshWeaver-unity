using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Modifiers
{
    public class VertwiseReset : VertwiseModifierBase
    {
        protected override bool ValidateTriangles => _resetScale;

        readonly bool _resetPosition;
        readonly bool _resetRotation;
        readonly bool _resetScale;

        public VertwiseReset(bool resetPosition, bool resetRotation, bool resetScale)
        {
            _resetPosition = resetPosition;
            _resetRotation = resetRotation;
            _resetScale = resetScale;
        }

        protected override IEnumerable<Vertie> Modify(IEnumerable<Vertie> vertices)
        {
            var first = vertices.FirstOrDefault();
            if (first == null) return vertices;

            var m = Matrix4x4.identity;
            var firstPosition = Vector3.zero;
            var firstRotation = Quaternion.identity;
            var firstScale = Vector3.one;
            
            Vertie SimpleModifier(Vertie vertie)
            {
                return vertie.WithTranslation(m.WithPosition(vertie.Vertex));
            }
            Vertie Modifier(Vertie vertie)
            {
                var t = vertie.Translation;
                var position = new Vector3(t.m03, t.m13, t.m23);
                var rotation = t.rotation;
                var scale = t.lossyScale;
                return vertie.WithTranslation(Matrix4x4.TRS(_resetPosition ? firstPosition : position,
                    _resetRotation ? firstRotation : rotation,
                    _resetScale ? firstScale : scale));
            }

            var modifier = _resetRotation && _resetScale && !_resetPosition ? (Func<Vertie, Vertie>)SimpleModifier : Modifier;
            return vertices.Select(modifier);
        }
    }
}