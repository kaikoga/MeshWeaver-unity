using Silksprite.MeshBuilder.Models.Paths.Shapes;

namespace Silksprite.MeshBuilder.Models.Paths
{
    public class ShapePathieFactory : IPathieFactory
    {
        readonly ShapePathieKind _kind;

        public ShapePathieFactory(ShapePathieKind kind)
        {
            _kind = kind;
        }

        public Pathie Build(LodMaskLayer lod)
        {
            switch (_kind)
            {
                case ShapePathieKind.Line:
                    return new LinePathieFactory().Build(lod);
                case ShapePathieKind.Rect:
                    return new RectPathieFactory().Build(lod);
            }

            return Pathie.Empty();
        }

        public enum ShapePathieKind
        {
            Line,
            Rect
        }
    }
}