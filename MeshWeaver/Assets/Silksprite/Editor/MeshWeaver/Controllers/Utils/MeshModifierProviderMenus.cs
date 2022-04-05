using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Meshes.Modifiers;
using Silksprite.MeshWeaver.Controllers.Modifiers;
using Silksprite.MeshWeaver.Utils;

namespace Silksprite.MeshWeaver.Controllers.Utils
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
            typeof(UvRemapperProvider),
            typeof(UvChannelRemapperProvider)
        );
    }
}