using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Controllers.Meshes.Modifiers;
using Silksprite.MeshBuilder.Controllers.Modifiers;
using Silksprite.MeshBuilder.Utils;

namespace Silksprite.MeshBuilder.Controllers.Utils
{
    public static class MeshModifierProviderMenus
    {
        public static readonly ModifierComponentPopupMenu<MeshModifierProviderBase> Menu = new ModifierComponentPopupMenu<MeshModifierProviderBase>
        (
            typeof(MeshReverseProvider),
            typeof(MeshRepeatProvider),
            typeof(MeshSubdivisionProvider),
            typeof(MeshResizeProvider),
            typeof(MeshNormalizeProvider),
            typeof(UvProjectorProvider)
        );
    }
}