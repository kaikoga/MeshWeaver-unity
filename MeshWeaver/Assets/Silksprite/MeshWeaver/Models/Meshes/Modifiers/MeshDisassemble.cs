using System.Collections.Generic;
using System.Linq;

namespace Silksprite.MeshWeaver.Models.Meshes.Modifiers
{
    public class MeshDisassemble : IMeshieModifier
    {
        public Meshie Modify(Meshie meshie)
        {
            var vertices = meshie.Vertices.ToArray(); 
            var gons = new List<Gon>();
            var index = 0;
            foreach (var gon in meshie.Gons)
            {
                gons.Add(new Gon(Enumerable.Range(index, gon.Indices.Count).ToArray(), gon.MaterialIndex));
                index += gon.Indices.Count;
            }
            var builder = Meshie.Builder();
            builder.Vertices.AddRange(meshie.Gons.SelectMany(gon => gon.Indices.Select(i => vertices[i])));
            builder.Gons.AddRange(gons);
            return builder.ToMeshie();
        }
    }
}