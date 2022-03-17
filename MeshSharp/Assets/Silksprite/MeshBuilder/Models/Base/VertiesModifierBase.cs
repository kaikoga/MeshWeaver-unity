using System.Linq;

namespace Silksprite.MeshBuilder.Models.Base
{
    public abstract class VertiesModifierBase : IMeshieModifier, IPathieModifier
    {
        public Meshie Modify(Meshie meshie)
        {
            var result = new Meshie();
            result.Vertices.AddRange(meshie.Vertices.Select(ModifyVertie));
            result.Indices.AddRange(meshie.Indices);
            return result;
        }

        public Pathie Modify(Pathie meshie)
        {
            var result = new Pathie();
            result.Vertices.AddRange(meshie.Vertices.Select(ModifyVertie));
            return result;
        }

        protected abstract Vertie ModifyVertie(Vertie vertie);
    }
}