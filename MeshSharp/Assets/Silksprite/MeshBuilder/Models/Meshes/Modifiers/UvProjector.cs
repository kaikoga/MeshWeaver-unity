using System.Linq;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Meshes.Modifiers
{
    public class UvProjector : MeshieModifier
    {
        readonly Matrix4x4 _translation;

        public UvProjector(Matrix4x4 translation)
        {
            _translation = translation;
        }

        public override Meshie Modify(Meshie meshie)
        {
            var result = new Meshie();
            result.Vertices.AddRange(meshie.Vertices.Select(v =>
            {
                var translation = _translation;
                return v.WithUv(translation.MultiplyPoint(v.Vertex));
            }));
            result.Indices.AddRange(meshie.Indices);
            return result;
        }
    }
}