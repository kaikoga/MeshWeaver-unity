using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Meshes.Modifiers;
using Silksprite.MeshWeaver.Controllers.Modifiers;
using Silksprite.MeshWeaver.Utils;

namespace Silksprite.MeshWeaver.Controllers.Utils
{
    public static class MeshModifierProviderMenus
    {
        public static readonly ModifierComponentPopupMenu<IMeshModifierProvider> Menu = new ModifierComponentPopupMenu<IMeshModifierProvider>
        (
            typeof(VertwiseNormalizeProvider),
            typeof(VertwiseTranslateProvider),
            typeof(VertwiseAlignProvider),
            typeof(VertwiseResetProvider),
            typeof(VertwiseQuantizeProvider),
            typeof(void),
            typeof(MeshReverseProvider),
            typeof(MeshRepeatProvider),
            typeof(MeshExtrudeProvider),
            typeof(MeshSubdivisionProvider),
            typeof(MeshDisassembleProvider),
            typeof(MeshCutoutBoundsProvider),
            typeof(MeshCutoutUvBoundsProvider),
            typeof(MeshCutoutColliderProvider),
            typeof(MeshSetMaterialProvider),
            typeof(void),
            typeof(UvProjectorProvider),
            typeof(UvRemapperProvider),
            typeof(UvChannelRemapperProvider),
            typeof(UvRandomizerProvider),
            typeof(UvChannelRandomizerProvider)
        );
    }
}