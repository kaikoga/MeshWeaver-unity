using System.Linq;
using Silksprite.MeshBuilder.Models.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Meshes.Modifiers
{
    public class MeshSubdivision : MeshModifier
    {
        readonly int _count;

        public MeshSubdivision(int count)
        {
            _count = count;
        }

        static readonly int[] SubdividedIndices = { 0, 1, 3, 1, 2, 4, 1, 4, 3, 3, 4, 5 };

        public override Meshie Modify(Meshie meshie)
        {
            if (_count < 1) return meshie;
            var count = Mathf.Clamp(_count, 1, 2);
            for (var n = 0; n < count; n++)
            {
                var result = new Meshie();
                for (var i = 0; i < meshie.Indices.Count; i += 3)
                {
                    var a = meshie.Vertices[meshie.Indices[i]];
                    var b = meshie.Vertices[meshie.Indices[i + 1]];
                    var c = meshie.Vertices[meshie.Indices[i + 2]];
                    var offset = result.Vertices.Count;
                    result.Vertices.Add(a);
                    result.Vertices.Add((a + b) * 0.5f);
                    result.Vertices.Add(b);
                    result.Vertices.Add((a + c) * 0.5f);
                    result.Vertices.Add((b + c) * 0.5f);
                    result.Vertices.Add(c);
                    result.Indices.AddRange(SubdividedIndices.Select(j => j + offset));
                }
                meshie = result;
            }
            return meshie;
        }
    }
}