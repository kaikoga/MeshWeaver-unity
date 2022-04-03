using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models.Meshes.Modifiers;
using Silksprite.MeshBuilder.Models.Paths.Modifiers;

namespace Silksprite.MeshBuilder.Models.Modifiers.Base
{
    public abstract class VertwiseModifierBase : IMeshieModifier, IPathieModifier
    {
        public Meshie Modify(Meshie meshie)
        {
            var result = Meshie.Builder();
            result.Vertices.AddRange(Modify(meshie.Vertices));
            result.Gons.AddRange(meshie.Gons);
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