using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Hikari.Patches;

namespace Hikari
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class HikariEntry : BaseUnityPlugin
    {
        // Meta
        private const string modGUID = "junko.konno.Hikari";
        private const string modName = "Hikari";
        private const string modVersion = "0.0.3";

        // Internal
        private readonly Harmony harmony = new Harmony(modGUID);
        private static HikariEntry Self;
        internal ManualLogSource logger;

        void Awake()
        {
            // NOTE: Probably can remove this?
            if (Self == null)
            {
                Self = this;
            }

            logger = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            // Header
            logger.LogInfo("Konnichiwa Sekai!");
            logger.LogInfo("Running on version " + modVersion + "!");

            // Config
            logger.LogInfo("Loading config.");
            Configuration.Config.Load();
            logger.LogInfo("Loaded!");

            // Patches
            logger.LogInfo("Applying Patches!");

            harmony.PatchAll(typeof(HikariEntry));
            harmony.PatchAll(typeof(MetricPatch));
            harmony.PatchAll(typeof(HealthDisplay));
            harmony.PatchAll(typeof(Crosshair));

            logger.LogInfo("Patch applied!");
        }
    }
}
