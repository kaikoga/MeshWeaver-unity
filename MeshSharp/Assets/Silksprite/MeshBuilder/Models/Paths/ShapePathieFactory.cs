using Silksprite.MeshBuilder.Models.Paths.Shapes;

namespace Silksprite.MeshBuilder.Models.Paths
{
    public class ShapePathieFactory
    {
        readonly ShapePathieKind _kind;

        public ShapePathieFactory(ShapePathieKind kind)
        {
            _kind = kind;
        }

        public Pathie Build(Pathie pathie)
        {
            switch (_kind)
            {
                case ShapePathieKind.Line:
                    return new LinePathieFactory().Build(pathie);
                case ShapePathieKind.Rect:
                    return new RectPathieFactory().Build(pathie);
            }

            return pathie;
        }

        public enum ShapePathieKind
        {
            Line,
            Rect
        }
    }
}