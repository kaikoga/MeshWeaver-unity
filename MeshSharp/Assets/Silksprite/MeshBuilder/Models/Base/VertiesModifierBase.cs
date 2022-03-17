using System.Collections.Generic;
using System.Linq;

namespace Silksprite.MeshBuilder.Models.Base
{
    public abstract class VertiesModifierBase : IMeshieModifier, IPathieModifier
    {
        public Meshie Modify(Meshie meshie)
        {
            var result = new Meshie();
            result.Vertices.AddRange(Modify(meshie.Vertices));
            result.Indices.AddRange(meshie.Indices);
            return result;
        }

        public Pathie Modify(Pathie meshie)
        {
            var result = new Pathie();
            result.Vertices.AddRange(meshie.Vertices.Select(Modify));
            return result;
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