using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Controllers.Modifiers;
using Silksprite.MeshBuilder.Controllers.Paths.Modifiers;
using Silksprite.MeshBuilder.Utils;

namespace Silksprite.MeshBuilder.Controllers.Utils
{
    public static class PathModifierProviderMenus
    {
        public static readonly ModifierComponentPopupMenu<PathModifierProviderBase> Menu = new ModifierComponentPopupMenu<PathModifierProviderBase>
        (
            typeof(VertwiseResizeProvider),
            typeof(VertwiseNormalizeProvider),
            typeof(void),
            typeof(PathRepeatProvider),
            typeof(PathSubdivisionProvider),
            typeof(void),
            typeof(UvGeneratorProvider),
            typeof(UvProjectorProvider),
            typeof(UvRemapperProvider)
        );
    }
}