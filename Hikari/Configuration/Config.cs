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
        private static ConfigEntry<float> config_CrossHairRed;
        private static ConfigEntry<float> config_CrossHairGreen;
        private static ConfigEntry<float> config_CrossHairBlue;
        private static ConfigEntry<float> config_CrossHairOutlineRed;
        private static ConfigEntry<float> config_CrossHairOutlineGreen;
        private static ConfigEntry<float> config_CrossHairOutlineBlue;
        private static ConfigEntry<float> config_CrossHairOutlineWidth;
        private static ConfigEntry<bool> config_UseMetric;

        // Access
        public static string CrossHairText => config_CrossHairText.Value;
        public static float CrossHairSize => config_CrossHairSize.Value;
        public static float CrossHairRed => config_CrossHairRed.Value;
        public static float CrossHairGreen => config_CrossHairGreen.Value;
        public static float CrossHairBlue => config_CrossHairBlue.Value;
        public static float CrossHairAlpha => config_CrossHairAlpha.Value;
        public static float CrossHairOutlineRed => config_CrossHairOutlineRed.Value;
        public static float CrossHairOutlineGreen => config_CrossHairOutlineGreen.Value;
        public static float CrossHairOutlineBlue => config_CrossHairOutlineBlue.Value;
        public static float CrossHairOutlineWidth => config_CrossHairOutlineWidth.Value;
        public static bool UseMetric => config_UseMetric.Value;

        // FNs
        public static void Load()
        {
            string configPath = Path.Combine(Paths.ConfigPath, "HikariPlus.cfg");
            config = new ConfigFile(configPath, true);

            InternalLoad();
        }

        public static void InternalLoad()
        {
            config_CrossHairText = config.Bind<string>("Hikari.Crosshair", "Text", "-  +  -", "The string for the Crosshair. ( Example: OwO, +, - )");
            config_CrossHairSize = config.Bind<float>("Hikari.Crosshair", "Size", 1.0f, "The scale for the Crosshair. (Default: 1.0)");
            config_CrossHairRed = config.Bind<float>("Hikari.Crosshair", "Color-Red", byte.MaxValue, "The transparency for the Crosshair. (Default: 255)");
            config_CrossHairGreen = config.Bind<float>("Hikari.Crosshair", "Color-Green", byte.MaxValue, "The transparency for the Crosshair. (Default: 255)");
            config_CrossHairBlue = config.Bind<float>("Hikari.Crosshair", "Color-Blue", byte.MaxValue, "The transparency for the Crosshair. (Default: 255)");
            config_CrossHairAlpha = config.Bind<float>("Hikari.Crosshair", "Alpha", 1.0f, "The transparency for the Crosshair. (Default: 1.0)");
            config_CrossHairOutlineRed = config.Bind<float>("Hikari.Crosshair", "Outline-Color-Red", 0f, "The red component of the crosshair outline color. (Default: 0)");
            config_CrossHairOutlineGreen = config.Bind<float>("Hikari.Crosshair", "Outline-Color-Green", 0f, "The green component of the crosshair outline color. (Default: 0)");
            config_CrossHairOutlineBlue = config.Bind<float>("Hikari.Crosshair", "Outline-Color-Blue", 0f, "The blue component of the crosshair outline color. (Default: 0)");
            config_CrossHairOutlineWidth = config.Bind<float>("Hikari.Crosshair", "Outline-Width", 1.0f, "The width of the crosshair outline. (Default: 1.0)");
            config_UseMetric = config.Bind<bool>("Hikari.Metric", "UseMetric", true, "Toggle between metric (kg) and imperial (lb) units. (Default: true)");
        }
    }
}
