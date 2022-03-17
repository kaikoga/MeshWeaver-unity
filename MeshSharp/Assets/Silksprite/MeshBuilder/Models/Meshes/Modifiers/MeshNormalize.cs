using System.Linq;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Meshes.Modifiers
{
    public class MeshNormalize : IMeshieModifier
    {
        public Meshie Modify(Meshie meshie)
        {
            var min = new Vector3(
                meshie.Vertices.Select(v => v.Vertex.x).Min(),
                meshie.Vertices.Select(v => v.Vertex.y).Min(),
                meshie.Vertices.Select(v => v.Vertex.z).Min());
            var max = new Vector3(
                meshie.Vertices.Select(v => v.Vertex.x).Max(),
                meshie.Vertices.Select(v => v.Vertex.y).Max(),
                meshie.Vertices.Select(v => v.Vertex.z).Max());
            var diff = max - min;
            if (diff.x == 0f) diff.x = 1f;
            if (diff.y == 0f) diff.y = 1f;
            if (diff.z == 0f) diff.z = 1f;
            var translation = Matrix4x4.Translate(-min) * Matrix4x4.Scale(diff).inverse; 
            var result = new Meshie();
            result.Vertices.AddRange(meshie.Vertices.Select(v => v.WithTranslation(translation * v.Translation)));
            result.Indices.AddRange(meshie.Indices);
            return result;
        }
    }
}