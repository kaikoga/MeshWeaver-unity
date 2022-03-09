using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Controllers.Paths;
using Silksprite.MeshBuilder.Utils;

namespace Silksprite.MeshBuilder.Controllers.Utils
{
    public static class PathProviderMenus
    {
        public static readonly ChildComponentPopupMenu<PathProvider> CollectionsMenu = new ChildComponentPopupMenu<PathProvider>(
            typeof(CompositePathProvider),
            typeof(PathReference),
            typeof(FixedPathProvider),
            typeof(ShapePathProvider),
            typeof(BakedPathProvider)
        );

        public static readonly ChildComponentPopupMenu<PathProvider> ElementsMenu = new ChildComponentPopupMenu<PathProvider>
        (
            typeof(PathReference),
            typeof(VertexProvider),
            typeof(ShapePathProvider),
            typeof(BakedPathProvider)
        );

        public static readonly ChildComponentPopupMenu<VertexProvider> VertexMenu = new ChildComponentPopupMenu<VertexProvider>
        (
            typeof(VertexProvider)
        );
    }
}