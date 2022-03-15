using System.Linq;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Meshes.Modifiers
{
    public class MeshResize : MeshieModifier
    {
        readonly Matrix4x4 _translation;

        public MeshResize(Matrix4x4 translation)
        {
            _translation = translation;
        }

        public override Meshie Modify(Meshie meshie)
        {
            var result = new Meshie();
            result.Vertices.AddRange(meshie.Vertices.Select(v => new Vertie(_translation * v.Translation, v.Uv, v.Culled)));
            result.Indices.AddRange(meshie.Indices);
            return result;
        }
    }
}