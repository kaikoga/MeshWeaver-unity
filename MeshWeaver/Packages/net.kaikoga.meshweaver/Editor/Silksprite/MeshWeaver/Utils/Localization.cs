using Silksprite.MeshWeaver.Controllers;

namespace Silksprite.MeshWeaver.Utils
{
    public static class Localization
    {
        public static string Lang
        {
            get => MeshWeaverSettings.Current.Lang;
            set => MeshWeaverSettings.Current.Lang = value;
        }

        internal static string PoPath(string lang)
        {
            return $"Packages/net.kaikoga.meshweaver/Editor/Silksprite/MeshWeaver/{lang}.po";
        }
    }
}