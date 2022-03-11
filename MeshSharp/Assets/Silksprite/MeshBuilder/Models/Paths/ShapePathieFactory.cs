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

        public Pathie Build()
        {
            switch (_kind)
            {
                case ShapePathieKind.Line:
                    return new LinePathieFactory().Build();
                case ShapePathieKind.Rect:
                    return new RectPathieFactory().Build();
            }

            return new Pathie();
        }

        public enum ShapePathieKind
        {
            Line,
            Rect
        }
    }
}