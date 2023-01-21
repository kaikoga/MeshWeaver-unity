using Silksprite.MeshWeaver.Controllers;

namespace Silksprite.MeshWeaver.Utils
{
    public static class Localization
    {
        public static string Lang
        {
            get => MeshWeaverSettings.Current.lang;
            set
            {
                MeshWeaverSettings.Current.lang = value;
                MeshWeaverSettings.ApplySettings();
            }
        }

        internal static string PoPath(string lang)
        {
            return $"Packages/net.kaikoga.meshweaver/Editor/Silksprite/MeshWeaver/{lang}.po";
        }
    }
}