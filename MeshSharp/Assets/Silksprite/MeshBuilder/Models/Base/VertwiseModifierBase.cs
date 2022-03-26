using System.Collections.Generic;
using System.Linq;

namespace Silksprite.MeshBuilder.Models.Base
{
    public abstract class VertwiseModifierBase : IMeshieModifier, IPathieModifier
    {
        public Meshie Modify(Meshie meshie)
        {
            var result = Meshie.Builder();
            result.Vertices.AddRange(Modify(meshie.Vertices));
            result.Indices.AddRange(meshie.Indices);
            return result.ToMeshie();
        }

        public Pathie Modify(Pathie meshie)
        {
            var result = Pathie.Builder();
            result.Vertices.AddRange(meshie.Vertices.Select(Modify));
            return result.ToPathie();
        }

        protected virtual IEnumerable<Vertie> Modify(IEnumerable<Vertie> vertices)
        {
            return vertices.Select(Modify);
        }

        protected virtual Vertie Modify(Vertie vertie)
        {
            return vertie;
        }
    }
}