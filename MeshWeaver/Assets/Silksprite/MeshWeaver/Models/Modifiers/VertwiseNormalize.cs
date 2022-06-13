using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Modifiers
{
    public class VertwiseNormalize : VertwiseModifierBase
    {
        readonly Bounds _bounds;

        public VertwiseNormalize(Bounds bounds)
        {
            _bounds = bounds;
        }

        protected override IEnumerable<Vertie> Modify(IEnumerable<Vertie> vertices)
        {
            if (!vertices.Any()) return vertices;

            var verts = vertices.ToArray();

            var min = new Vector3(
                verts.Select(v => v.Vertex.x).Min(),
                verts.Select(v => v.Vertex.y).Min(),
                verts.Select(v => v.Vertex.z).Min());
            var max = new Vector3(
                verts.Select(v => v.Vertex.x).Max(),
                verts.Select(v => v.Vertex.y).Max(),
                verts.Select(v => v.Vertex.z).Max());
            var diff = max - min;
            if (diff.x == 0f) diff.x = 1f;
            if (diff.y == 0f) diff.y = 1f;
            if (diff.z == 0f) diff.z = 1f;
            var translation = Matrix4x4.Translate(_bounds.min) * Matrix4x4.Scale(_bounds.size) * Matrix4x4.Scale(diff).inverse * Matrix4x4.Translate(-min);
            
            return verts.Select(v => v.WithTranslation(translation * v.Translation));
        }
    }
}