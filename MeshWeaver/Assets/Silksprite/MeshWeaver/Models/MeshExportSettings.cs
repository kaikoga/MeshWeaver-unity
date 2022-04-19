namespace Silksprite.MeshWeaver.Models
{
    public readonly struct MeshExportSettings
    {
        public readonly NormalGeneratorKind NormalGenerator;
        public readonly float NormalAngle;

        public MeshExportSettings(NormalGeneratorKind normalGenerator, float normalAngle)
        {
            NormalGenerator = normalGenerator;
            NormalAngle = normalAngle;
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
        }
    }
}