using System;

namespace Soncoord.Infrastructure
{
    public class Globals
    {
        private static readonly string LocalAppData = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Soncoord";

        public static readonly string SongsPath = $"{LocalAppData}\\Songs";
        public static readonly string SongSettingsPath = $"{SongsPath}\\Settings";

        public static readonly string PlayerPath = $"{LocalAppData}\\Player";
        public static readonly string PlayerOutputSettingsFile = $"{PlayerPath}\\settings.json";

        public static readonly string TracksSourcePath = $"D:\\Song Library\\";
    }
}
