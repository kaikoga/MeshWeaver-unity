using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models.Base;
using Silksprite.MeshBuilder.Models.Extensions;

namespace Silksprite.MeshBuilder.Models.Paths.Modifiers
{
    public class PathBevel : IPathieModifier
    {
        public PathBevel()
        {
        }

        public Pathie Modify(Pathie pathie)
        {
            var vertices = pathie.Active.Vertices;
            if (vertices.Length < 2) return pathie;

            IEnumerable<Vertie> Bevel(Vertie a, Vertie b, Vertie c)
            {
                yield return a * 0.4f + b * 0.6f;
                yield return a * 0.1f + b * 0.8f + c * 0.1f;
                yield return b * 0.6f + c * 0.4f;
            }

            var builder = Pathie.Builder();
            builder.Vertices.Add(vertices[0]);
            builder.Vertices.AddRange(vertices.EachSlidingTrio(Bevel).SelectMany(v => v));
            builder.Vertices.Add(vertices[vertices.Length - 1]);
            return builder.ToPathie();
        }
    }
}