using HarmonyLib;
using TMPro;
using UnityEngine;

namespace Hikari.Patches
{
    [HarmonyPatch(typeof(HUDManager))]
    internal class Crosshair
    {

        // FNs
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        static void Start(ref HUDManager __instance)
        {
            GameObject crossHairObject = new GameObject("Hikari.Crosshair.Hijack");
            crossHairObject.AddComponent<RectTransform>();

            TextMeshProUGUI crossHairText = crossHairObject.AddComponent<TextMeshProUGUI>();
            crossHairText.font = __instance.weightCounter.font;
            crossHairText.fontSize = 32;
            crossHairText.text = "-  +  -";
            crossHairText.alignment = TextAlignmentOptions.Center;
            crossHairText.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
            crossHairText.enabled = true;

            RectTransform crossHairTransform = crossHairText.rectTransform;
            crossHairTransform.SetParent(__instance.PTTIcon.transform.parent.parent.parent.Find("PlayerCursor").Find("Cursor").transform, worldPositionStays: false);
            crossHairTransform.anchoredPosition = new Vector2(0, 0);
            crossHairTransform.localPosition = new Vector3(0, 0, 0);
            crossHairTransform.offsetMin = new Vector2(-500, -500);
            crossHairTransform.offsetMax = new Vector2(500, 500);
        }
    }
}
