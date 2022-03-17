using System.Linq;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Meshes.Modifiers
{
    public class MeshResize : IMeshieModifier
    {
        readonly Matrix4x4 _translation;

        public MeshResize(Matrix4x4 translation)
        {
            _translation = translation;
        }

        public Meshie Modify(Meshie meshie)
        {
            var result = new Meshie();
            result.Vertices.AddRange(meshie.Vertices.Select(v => v.WithTranslation(_translation * v.Translation)));
            result.Indices.AddRange(meshie.Indices);
            return result;
        }
    }
}