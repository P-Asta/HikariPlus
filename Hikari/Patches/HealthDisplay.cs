using HarmonyLib;
using TMPro;
using UnityEngine;

namespace Hikari.Patches
{
    [HarmonyPatch(typeof(HUDManager))]
    internal class HealthDisplay
    {
        // Internal
        private static TextMeshProUGUI healthText;

        // Public
        public static int currentHealthValue = 100;

        // Color
        private static Color32 ShitsBussin = new Color32(0, byte.MaxValue, 0, byte.MaxValue);                     // 100
        private static Color32 CapBussin = new Color32(byte.MaxValue / 25, byte.MaxValue, 0, byte.MaxValue);      // 75
        private static Color32 MidBussin = new Color32(byte.MaxValue / 50, byte.MaxValue / 25, 0, byte.MaxValue); // 50
        private static Color32 AintBussin = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);                      // 25

        // FNs
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        static void Start(ref HUDManager __instance)
        {
            GameObject healthObject = new GameObject(typeof(HealthDisplay).ToString());
            healthObject.AddComponent<RectTransform>();

            healthText = healthObject.AddComponent<TextMeshProUGUI>();
            healthText.font = __instance.weightCounter.font;
            healthText.fontSize = 16f;
            healthText.text = "100 HP";
            healthText.alignment = __instance.weightCounter.alignment;
            healthText.color = ShitsBussin;
            healthText.overflowMode = TextOverflowModes.Overflow;
            healthText.enabled = true;

            RectTransform healthTransform = healthText.rectTransform;
            healthTransform.SetParent(__instance.weightCounter.transform, worldPositionStays: false);
            healthTransform.anchoredPosition = new Vector2(30f, 15f);
        }

        [HarmonyPatch("UpdateHealthUI")]
        [HarmonyPostfix]
        static void Update(ref HUDManager __instance, int health, bool hurtPlayer = true)
        {
            // NOTE: Maybe do a fancy gradient color?
            // For now let's just use a simple GREEN - RED analogue color.
            if (health <= 100)
            {
                healthText.color = ShitsBussin;
            }

            if (health <= 75)
            {
                healthText.color = CapBussin;
            }

            if (health <= 50)
            {
                healthText.color = MidBussin;
            }

            if (health < 25)
            {
                healthText.color = AintBussin;
            }

            healthText.text = $"{health} HP";

            // THIS IS FUCKING RETARDED BUT WHATEVER ITS 4AM I COULDNT BE ARSED TO THINK
        }
    }
}
