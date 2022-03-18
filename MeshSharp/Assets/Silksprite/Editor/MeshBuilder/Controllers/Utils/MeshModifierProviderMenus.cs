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
            typeof(VertwiseNormalizeProvider),
            typeof(VertwiseTranslateProvider),
            typeof(void),
            typeof(MeshReverseProvider),
            typeof(MeshRepeatProvider),
            typeof(MeshSubdivisionProvider),
            typeof(void),
            typeof(UvProjectorProvider),
            typeof(UvRemapperProvider)
        );
    }
}