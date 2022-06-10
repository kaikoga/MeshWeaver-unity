using System.Linq;

namespace Silksprite.MeshWeaver.Models.Paths.Modifiers
{
    public class PathReverse : IPathieModifier
    {
        public Pathie Modify(Pathie pathie) => new Pathie(pathie.Vertices.Reverse(), pathie.isLoop);
    }
}