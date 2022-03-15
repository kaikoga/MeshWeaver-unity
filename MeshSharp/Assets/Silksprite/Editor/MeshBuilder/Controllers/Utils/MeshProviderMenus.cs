using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Controllers.Meshes;
using Silksprite.MeshBuilder.Utils;

namespace Silksprite.MeshBuilder.Controllers.Utils
{
    public static class MeshProviderMenus
    {
        public static readonly ChildComponentPopupMenu<MeshProvider> Menu = new ChildComponentPopupMenu<MeshProvider>(
            typeof(CompositeMeshProvider),
            typeof(PolygonMeshProvider),
            typeof(MatrixMeshProvider),
            typeof(BakedMeshProvider)
        );
    }
}