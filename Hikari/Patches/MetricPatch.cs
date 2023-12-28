using HarmonyLib;
using TMPro;
using UnityEngine;


namespace Hikari.Patches
{
    [HarmonyPatch(typeof(HUDManager))]
    internal class MetricPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void patchMetricPostUpdate(ref HUDManager __instance, ref TextMeshProUGUI ___weightCounter, ref Animator ___weightCounterAnimator)
        {
            if (GameNetworkManager.Instance == null || GameNetworkManager.Instance.localPlayerController == null || GameNetworkManager.Instance.localPlayerController == null)
            {
                return;
            }

            if (___weightCounter != null && ___weightCounterAnimator != null)
            {
                float weight_lb = Mathf.RoundToInt(Mathf.Clamp(GameNetworkManager.Instance.localPlayerController.carryWeight - 1f, 0f, 100f) * 105f);
                float weight_kg = weight_lb / 2.205f;

                ___weightCounter.text = weight_kg.ToString("F2") + " kg";
                ___weightCounterAnimator.SetFloat("weight", weight_kg / 130f);
            }
        }
    }


}
