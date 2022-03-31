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
            typeof(void),
            typeof(ShapePathProvider),
            typeof(RevolutionPathProvider),
            typeof(void),
            typeof(BakedPathProvider)
        );

        public static readonly ChildComponentPopupMenu<PathProvider> ElementsMenu = new ChildComponentPopupMenu<PathProvider>
        (
            typeof(PathReference),
            typeof(void),
            typeof(VertexProvider),
            typeof(ShapePathProvider),
            typeof(RevolutionPathProvider),
            typeof(void),
            typeof(BakedPathProvider)
        );

        public static readonly ChildComponentPopupMenu<VertexProvider> VertexMenu = new ChildComponentPopupMenu<VertexProvider>
        (
            typeof(VertexProvider)
        );
    }
}