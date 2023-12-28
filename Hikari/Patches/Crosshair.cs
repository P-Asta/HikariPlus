using HarmonyLib;
using Hikari.Configuration;
using TMPro;
using UnityEngine;

namespace Hikari.Patches
{
    [HarmonyPatch(typeof(HUDManager))]
    internal class Crosshair
    {
        // Crosshair
        private static GameObject crossHairObject;
        private static TextMeshProUGUI crossHairText;
        private static RectTransform crossHairTransform;


        // Patch
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        static void Start(ref HUDManager __instance)
        {
            crossHairObject = new GameObject("Hikari.Crosshair.Display");
            crossHairObject.AddComponent<RectTransform>();

            crossHairText = crossHairObject.AddComponent<TextMeshProUGUI>();
            crossHairText.font = __instance.weightCounter.font;
            crossHairText.fontSize = 32 * Config.CrossHairSize;
            crossHairText.text = Config.CrossHairText;
            crossHairText.alignment = TextAlignmentOptions.Center;
            crossHairText.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)(255f * Config.CrossHairAlpha));
            crossHairText.outlineColor = Color.black;
            crossHairText.outlineWidth = 32 * Config.CrossHairSize;
            crossHairText.enabled = true;

            crossHairTransform = crossHairText.rectTransform;
            crossHairTransform.SetParent(__instance.PTTIcon.transform.parent.parent.parent.Find("PlayerCursor").Find("Cursor").transform, worldPositionStays: false);
            crossHairTransform.anchoredPosition = new Vector2(0, 0);
            crossHairTransform.localPosition = new Vector3(0, 0, 0);
            crossHairTransform.offsetMin = new Vector2(-500, -500);
            crossHairTransform.offsetMax = new Vector2(500, 500);
        }

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void Update(ref HUDManager __instance)
        {
            if (GameNetworkManager.Instance == null || GameNetworkManager.Instance.localPlayerController == null || GameNetworkManager.Instance.localPlayerController == null)
            {
                return;
            }

            crossHairText.enabled = !GameNetworkManager.Instance.localPlayerController.isPlayerDead;
        }
    }
}
