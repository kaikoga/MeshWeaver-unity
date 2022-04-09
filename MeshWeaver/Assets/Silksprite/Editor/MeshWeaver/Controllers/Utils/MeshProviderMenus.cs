using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Meshes;
using Silksprite.MeshWeaver.Utils;

namespace Silksprite.MeshWeaver.Controllers.Utils
{
    public static class MeshProviderMenus
    {
        public static readonly ChildComponentPopupMenu<MeshProvider> Menu = new ChildComponentPopupMenu<MeshProvider>(
            typeof(CompositeMeshProvider),
            typeof(void),
            typeof(PolygonMeshProvider),
            typeof(MatrixMeshProvider),
            typeof(PillarMeshProvider),
            typeof(StitchMeshProvider),
            typeof(void),
            typeof(BakedMeshProvider)
        );
    }
}