using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;

namespace Silksprite.MeshWeaver.Models.Modifiers.Base
{
    public abstract class VertwiseModifierBase : IMeshieModifier, IPathieModifier
    {
        protected virtual bool ValidateTriangles => false;

        public Meshie Modify(Meshie meshie)
        {
            var result = Meshie.Builder();
            result.Vertices.AddRange(Modify(meshie.Vertices));
            if (ValidateTriangles)
            {
                result.AddTriangles(meshie.Gons);
            }
            else
            {
                result.Gons.AddRange(meshie.Gons);
            }
            return result.ToMeshie();
        }

        public Pathie Modify(Pathie pathie)
        {
            var result = Pathie.Builder();
            result.Vertices.AddRange(Modify(pathie.Vertices));
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