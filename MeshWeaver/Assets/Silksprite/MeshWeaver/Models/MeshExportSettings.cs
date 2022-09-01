namespace Silksprite.MeshWeaver.Models
{
    public readonly struct MeshExportSettings
    {
        public readonly NormalGeneratorKind NormalGenerator;
        public readonly float NormalAngle;
        public readonly LightmapGeneratorKind LightmapGenerator;

        public MeshExportSettings(NormalGeneratorKind normalGenerator, float normalAngle, LightmapGeneratorKind lightmapGenerator)
        {
            NormalGenerator = normalGenerator;
            NormalAngle = normalAngle;
            LightmapGenerator = lightmapGenerator;
        }

        public enum NormalGeneratorKind
        {
            Default = 0,
            Up = 1,
            Down = 2,
            Sphere = 3,
            Cylinder = 4,
            Smooth = 64,
            SmoothHigh = 65,
            None = -1,
        }
        
        public enum LightmapGeneratorKind
        {
            Default = 0,
            None = -1,
        }
    }
}