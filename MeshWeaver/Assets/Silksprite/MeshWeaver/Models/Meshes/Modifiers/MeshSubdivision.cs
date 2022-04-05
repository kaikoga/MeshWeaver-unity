using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Meshes.Modifiers
{
    public class MeshSubdivision : IMeshieModifier
    {
        readonly int _count;

        public MeshSubdivision(int count)
        {
            _count = count;
        }

        static readonly int[] SubdividedIndices = { 0, 1, 3, 1, 2, 4, 1, 4, 3, 3, 4, 5 };

        public Meshie Modify(Meshie meshie)
        {
            if (_count < 1) return meshie;
            var count = Mathf.Clamp(_count, 1, 2);
            var result = Meshie.Builder(meshie);
            for (var n = 0; n < count; n++)
            {
                var builder = Meshie.Builder();
                foreach (var gon in result.Gons)
                {
                    var a = result.Vertices[gon[0]];
                    var b = result.Vertices[gon[1]];
                    var c = result.Vertices[gon[2]];
                    var offset = builder.Vertices.Count;
                    builder.Vertices.Add(a);
                    builder.Vertices.Add((a + b) * 0.5f);
                    builder.Vertices.Add(b);
                    builder.Vertices.Add((a + c) * 0.5f);
                    builder.Vertices.Add((b + c) * 0.5f);
                    builder.Vertices.Add(c);
                    builder.AddTriangles(SubdividedIndices.Select(j => j + offset), gon.MaterialIndex);
                }
                result = builder;
            }
            return result.ToMeshie();
        }
    }
}