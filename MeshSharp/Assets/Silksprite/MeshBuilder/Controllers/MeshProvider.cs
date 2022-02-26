using System.Linq;
using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Controllers
{
    public class MeshProvider : GeometryProvider
    {
        public PathProvider[] pathProviders;

        public Meshie ToMeshie()
        {
            var inverse = Translation.inverse;
            var pathie = new Pathie();
            foreach (var pathProvider in pathProviders.Where(c => c.isActiveAndEnabled))
            {
                pathie.Concat(pathProvider.ToPathie(), inverse * pathProvider.Translation);
            }

            var meshie = new Meshie();
            if (pathie.Vertices.Count < 3) return meshie;

            meshie.Vertices.AddRange(pathie.Vertices);
            meshie.Indices.AddRange(Enumerable.Range(1, pathie.Vertices.Count - 2).SelectMany(i => new[] { 0, i, i + 1 }));
            return meshie;
        }
    }
}