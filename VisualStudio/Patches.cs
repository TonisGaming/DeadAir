using Il2Cpp;
using HarmonyLib;
using UnityEngine;
using Il2CppSteamworks;
using Il2CppTLD.Gear;

namespace DeadAir
{
    internal class Patches
    {
        [HarmonyPatch(typeof(Panel_Inventory), nameof(Panel_Inventory.Initialize))]
        internal class DeadAirInitialization
        {
            private static void Postfix(Panel_Inventory __instance)
            {
                DeadAirUtils.inventory = __instance;
                DAFunctionalities.InitializeMTB(__instance.m_ItemDescriptionPage);
            }
        }
        [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
        internal class UpdateInventoryButton
        {
            private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
            {
                if (__instance != InterfaceManager.GetPanel<Panel_Inventory>()?.m_ItemDescriptionPage) return;
                DAFunctionalities.canisterItem = gi?.GetComponent<GearItem>();


                if (gi.name == "GEAR_Canister" && gi.m_CurrentHP == 0)
                {
                    DAFunctionalities.SetCanisterRefillActive(true);
                }
                else
                {
                    DAFunctionalities.SetCanisterRefillActive(false);
                }

                if (gi.name == "GEAR_ImprovisedFilter" && gi.m_CurrentHP == 0)
                {
                    DAFunctionalities.SetCanisterRefillActive(true);
                }
                else
                {
                    DAFunctionalities.SetCanisterRefillActive(false);
                }

            }
        }
        [HarmonyPatch(typeof(StartGear), "AddAllToInventory")]
        internal class StartGear_AddAllToInventory
        {
            private static void Postfix()
            {

                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(DeadAirUtils.canister, 1).m_RespiratorCanister.m_ProtectionDurationRTSeconds = 600;
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(DeadAirUtils.canister2, 1).m_RespiratorCanister.m_ProtectionDurationRTSeconds = 600;
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(DeadAirUtils.canisterimprov, 1).m_RespiratorCanister.m_ProtectionDurationRTSeconds = 300;
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(DeadAirUtils.respirator, 1).m_Respirator.m_ActiveZones = 1;
                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.respirator, 1).m_Respirator.m_AttachedCanister.m_ProtectionDurationRTSeconds = 600;
            }
        }

        
    }
}
    