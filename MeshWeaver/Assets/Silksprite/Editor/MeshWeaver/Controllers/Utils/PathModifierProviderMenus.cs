using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Modifiers;
using Silksprite.MeshWeaver.Controllers.Paths.Modifiers;
using Silksprite.MeshWeaver.Utils;

namespace Silksprite.MeshWeaver.Controllers.Utils
{
    public static class PathModifierProviderMenus
    {
        public static readonly ModifierComponentPopupMenu<PathModifierProviderBase> Menu = new ModifierComponentPopupMenu<PathModifierProviderBase>
        (
            typeof(VertwiseNormalizeProvider),
            typeof(VertwiseTranslateProvider),
            typeof(VertwiseAlignProvider),
            typeof(VertwiseResetProvider),
            typeof(void),
            typeof(PathRepeatProvider),
            typeof(PathSubdivisionProvider),
            typeof(PathOptimizeProvider),
            typeof(PathBevelProvider),
            typeof(PathChainProvider),
            typeof(void),
            typeof(UvGeneratorProvider),
            typeof(UvProjectorProvider),
            typeof(UvRemapperProvider),
            typeof(UvChannelRemapperProvider)
        );
    }
}