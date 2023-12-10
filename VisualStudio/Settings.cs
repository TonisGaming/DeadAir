using ModSettings;

namespace DeadAir
{
    internal class Settings : JsonModSettings
    {
        internal static Settings instance = new Settings();

        [Section("Options")]

        [Name("Enable Mod")]
        [Description("Choose If You Want The Mod To Be On Or Off")]
        public bool useMod = true;
    }
}
