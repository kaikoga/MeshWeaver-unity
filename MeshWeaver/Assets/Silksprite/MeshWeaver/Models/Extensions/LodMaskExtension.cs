namespace Silksprite.MeshWeaver.Models.Extensions
{
    public static class LodMaskExtension
    {
        public static bool HasLayer(this LodMask lodMask, LodMaskLayer layer)
        {
            return lodMask.HasFlag((LodMask)layer);
        }
    }
}