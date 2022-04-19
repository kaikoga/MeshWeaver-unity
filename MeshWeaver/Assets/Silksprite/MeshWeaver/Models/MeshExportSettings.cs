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
            Default
        }
    }
}