using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Controllers.Paths.Modifiers;
using Silksprite.MeshBuilder.Utils;

namespace Silksprite.MeshBuilder.Controllers.Utils
{
    public static class PathModifierProviderMenus
    {
        public static readonly ModifierComponentPopupMenu<PathModifierProvider> Menu = new ModifierComponentPopupMenu<PathModifierProvider>
        (
            typeof(PathRepeatProvider),
            typeof(PathSubdivisionProvider),
            typeof(UvGeneratorProvider),
            typeof(UvRemapperProvider)
        );
    }
}