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
            crossHairText.fontSize = 32 * (Config.CrossHairSize);
            crossHairText.text = Config.CrossHairText;
            crossHairText.alignment = TextAlignmentOptions.Center;
            crossHairText.color = new Color32((byte)(Config.CrossHairRed), (byte)(Config.CrossHairGreen), (byte)(Config.CrossHairBlue), (byte)(255f * Config.CrossHairAlpha));
            if (Config.CrossHairOutlineWidth != 0)
            {
                Material mat = crossHairText.fontSharedMaterial;

                // 기존 FaceDilate 값 (너무 크면 박스 모양이 생김)
                mat.SetFloat(ShaderUtilities.ID_FaceDilate, Mathf.Clamp(Config.CrossHairOutlineWidth, -1f, 1f));

                // 아웃라인 색상 반영
                Color outlineColor = new Color32((byte)Config.CrossHairOutlineRed,
                                                (byte)Config.CrossHairOutlineGreen,
                                                (byte)Config.CrossHairOutlineBlue,
                                                (byte)(255f * Config.CrossHairAlpha));

                crossHairText.outlineColor = outlineColor;
                mat.SetColor(ShaderUtilities.ID_OutlineColor, outlineColor); // 추가 설정

                // 불필요한 Underlay 제거
                mat.SetFloat(ShaderUtilities.ID_UnderlayOffsetX, 0f);
                mat.SetFloat(ShaderUtilities.ID_UnderlayOffsetY, 0f);
                mat.SetFloat(ShaderUtilities.ID_UnderlaySoftness, 0f);
                mat.SetColor(ShaderUtilities.ID_UnderlayColor, new Color(0, 0, 0, 0)); // 완전 투명하게 설정
            }


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
