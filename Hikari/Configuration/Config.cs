using BepInEx;
using BepInEx.Configuration;
using System.IO;


namespace Hikari.Configuration
{
    internal static class Config
    {
        private static ConfigFile config;

        // [Configuration]
        // Crosshair
        private static ConfigEntry<string> config_CrossHairText;
        private static ConfigEntry<float> config_CrossHairSize;
        private static ConfigEntry<float> config_CrossHairAlpha;

        // Access
        public static string CrossHairText => config_CrossHairText.Value;
        public static float CrossHairSize => config_CrossHairSize.Value;
        public static float CrossHairAlpha => config_CrossHairAlpha.Value;

        // FNs
        public static void Load()
        {
            string configPath = Path.Combine(Paths.ConfigPath, "Hikari.cfg");
            config = new ConfigFile(configPath, true);

            InternalLoad();
        }

        public static void InternalLoad()
        {
            config_CrossHairText = config.Bind<string>("Hikari.Crosshair", "Text", "-  +  -", "The string for the Crosshair. ( Example: OwO, +, - )");
            config_CrossHairSize = config.Bind<float>("Hikari.Crosshair", "Size", 1.0f, "The scale for the Crosshair. (Default: 1.0)");
            config_CrossHairAlpha = config.Bind<float>("Hikari.Crosshair", "Alpha", 1.0f, "The transparency for the Crosshair. (Default: 1.0)");
        }
    }
}
