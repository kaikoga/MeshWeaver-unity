using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Controllers.Meshes.Modifiers;
using Silksprite.MeshBuilder.Controllers.Paths.Modifiers;
using Silksprite.MeshBuilder.Utils;

namespace Silksprite.MeshBuilder.Controllers.Utils
{
    public static class MeshModifierProviderMenus
    {
        public static readonly ModifierComponentPopupMenu<MeshModifierProvider> Menu = new ModifierComponentPopupMenu<MeshModifierProvider>
        (
            typeof(MeshReverseProvider),
            typeof(MeshRepeatProvider),
            typeof(MeshSubdivisionProvider),
            typeof(UvProjectorProvider)
        );
    }
}