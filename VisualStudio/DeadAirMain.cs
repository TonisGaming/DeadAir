using Il2Cpp;
using Il2CppHoloville.HOTween.Core.Easing;
using DeadAir;
using MelonLoader;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Il2CppTLD.Gear;
using Il2CppTLD;
using Il2CppTLD.Gameplay.Condition;
using Il2CppNodeCanvas.BehaviourTrees;
using JetBrains.Annotations;
using Il2CppRewired;
using UnityEngine.UI;
using Il2CppAK.Wwise;

namespace ModNamespace
{
    internal sealed class DeadAirMain : MelonMod
    {

        public static bool isLoaded;

        private static bool addedCustomComponents;

        public override void OnInitializeMelon()
        {
            MelonLoader.MelonLogger.Msg(System.ConsoleColor.Yellow, "I've heard tales of singing pipes");
            MelonLoader.MelonLogger.Msg(System.ConsoleColor.Yellow, "Maybe breathing these fumes ain't the best Bourbon");
            MelonLoader.MelonLogger.Msg(System.ConsoleColor.Green, "Canister Refill Loaded!");
            Settings.instance.AddToModSettings("Dead Air");
        }

        public override void OnSceneWasInitialized(int level, string name)
        {
            if (DeadAirUtils.IsScenePlayable(name))
            {
                isLoaded = true;
                if (Settings.instance.useMod == true)
                {
                    DoStuffWithGear();

                    GameManager.GetChemicalPoisoningComponent().m_InHazardZone = true;
                    GameManager.GetChemicalPoisoningComponent().m_ActiveZones = 1;

                    if (DeadAirUtils.IsSceneSafe(name))
                    {
                        AirPollutionStop();
                    }
                    else if (DeadAirUtils.IsSceneHouse(name))
                    {
                        AirPollutionInside();
                    }
                    else if (DeadAirUtils.IsSceneCinderHills(name))
                    {
                        AirPollutionCinderHills();
                    }
                    else
                    {
                        AirPollutionOutside();
                    }
                }
                else
                {
                    DoStuffWithGear();
                }

            }
   
        }
        public override void OnUpdate()
        {
            if (!isLoaded || GameManager.GetPlayerManagerComponent() == null) return;


            if (GameManager.GetPlayerManagerComponent().m_PickupGearItem == DeadAirUtils.canister)
            {
                
                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.canister, 1).m_RespiratorCanister.m_ProtectionDurationRTSeconds = 600;

            }
        }

        private static void AirPollutionOutside()
        {

            if (GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.respirator, 1) == true)
            {
                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.respirator, 1).m_Respirator.m_ActiveZones = 0;

                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.respirator, 1).m_Respirator.m_AttachedCanister.m_ProtectionDurationRTSeconds = 600;
                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.respirator, 1).m_Respirator.m_ActiveZones = 1;
            }
            

            if(GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.canister, 1) == true)
            {
                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.canister, 1).m_RespiratorCanister.m_ProtectionDurationRTSeconds = 600;
            }

            if (GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.canisterimprov, 1) == true)
            {
                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.canisterimprov, 1).m_RespiratorCanister.m_ProtectionDurationRTSeconds = 300;
            }
            

            GameManager.GetChemicalPoisoningComponent().m_InHazardZone = true;
            GameManager.GetChemicalPoisoningComponent().m_ActiveZones = 1;
            GameManager.GetChemicalPoisoningComponent().m_ClothingHPLostPerHour = 0.05f;
            GameManager.GetChemicalPoisoningComponent().m_ClothingDamageRegion = ClothingRegion.NumRegions;
            GameManager.GetChemicalPoisoningComponent().m_ToxicityGainedPerHour = 300;
            GameManager.GetChemicalPoisoningComponent().m_ToxicityLostPerHour = 2.5f;

        }

        private static void AirPollutionInside()
        {

            if (GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.respirator, 1) == true)
            {
                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.respirator, 1).m_Respirator.m_ActiveZones = 0;

                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.respirator, 1).m_Respirator.m_AttachedCanister.m_ProtectionDurationRTSeconds = 900;
                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.respirator, 1).m_Respirator.m_ActiveZones = 1;
            }

            if (GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.canister, 1) == true)
            {
                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.canister, 1).m_RespiratorCanister.m_ProtectionDurationRTSeconds = 1200;
            }

            if (GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.canisterimprov, 1) == true)
            {
                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.canisterimprov, 1).m_RespiratorCanister.m_ProtectionDurationRTSeconds = 600;
            }

            GameManager.GetChemicalPoisoningComponent().m_InHazardZone = true;
            GameManager.GetChemicalPoisoningComponent().m_ActiveZones = 1;
            GameManager.GetChemicalPoisoningComponent().m_ClothingHPLostPerHour = 0.01f;
            GameManager.GetChemicalPoisoningComponent().m_ClothingDamageRegion = ClothingRegion.NumRegions;
            GameManager.GetChemicalPoisoningComponent().m_ToxicityGainedPerHour = 120;
            GameManager.GetChemicalPoisoningComponent().m_ToxicityLostPerHour = 15f;

        }

        private static void AirPollutionCinderHills()
        {
            if (GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.respirator, 1) == true)
            {
                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.respirator, 1).m_Respirator.m_ActiveZones = 0;

                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.respirator, 1).m_Respirator.m_AttachedCanister.m_ProtectionDurationRTSeconds = 75;
                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.respirator, 1).m_Respirator.m_ActiveZones = 1;
            }

            if (GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.canister, 1) == true)
            {
                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.canister, 1).m_RespiratorCanister.m_ProtectionDurationRTSeconds = 75;
            }

            if (GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.canisterimprov, 1) == true)
            {
                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.canisterimprov, 1).m_RespiratorCanister.m_ProtectionDurationRTSeconds = 30;
            }

            GameManager.GetChemicalPoisoningComponent().m_InHazardZone = true;
            GameManager.GetChemicalPoisoningComponent().m_ActiveZones = 1;
            GameManager.GetChemicalPoisoningComponent().m_ClothingHPLostPerHour = 1.5f;
            GameManager.GetChemicalPoisoningComponent().m_ClothingDamageRegion = ClothingRegion.NumRegions;
            GameManager.GetChemicalPoisoningComponent().m_ToxicityGainedPerHour = 600;
            GameManager.GetChemicalPoisoningComponent().m_ToxicityLostPerHour = 1f;

        }

        private static void AirPollutionStop()
        {

            if (GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.respirator, 1) == true)
            {
                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.respirator, 1).m_Respirator.m_ActiveZones = 0;

                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.respirator, 1).m_Respirator.m_AttachedCanister.m_ProtectionDurationRTSeconds = 600;
                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.respirator, 1).m_Respirator.m_ActiveZones = 0;
            }

            if (GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.canister, 1) == true)
            {
                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.canister, 1).m_RespiratorCanister.m_ProtectionDurationRTSeconds = 600;
            }

            if (GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.canisterimprov, 1) == true)
            {
                GameManager.GetInventoryComponent().GearInInventory(DeadAirUtils.canisterimprov, 1).m_RespiratorCanister.m_ProtectionDurationRTSeconds = 300;
            }


            GameManager.GetChemicalPoisoningComponent().m_InHazardZone = false;
            GameManager.GetChemicalPoisoningComponent().m_ActiveZones = 0;
            GameManager.GetChemicalPoisoningComponent().m_ClothingHPLostPerHour = 0f;
            GameManager.GetChemicalPoisoningComponent().m_ToxicityGainedPerHour = 300;
            GameManager.GetChemicalPoisoningComponent().m_ToxicityLostPerHour = 2.5f;

        }

        private static void DoStuffWithGear()
        {
            if (!addedCustomComponents)
            {
                GameObject gear;
                GearItem gearItem;

                string gear1 = "ImprovisedFilter";

                gear = GearItem.LoadGearItemPrefab("GEAR_" + gear1).gameObject;
                gearItem = GearItem.LoadGearItemPrefab("GEAR_" + gear1);

                gear.AddComponent<RespiratorCanister>();
                gear.GetComponent<RespiratorCanister>().m_GearItem = gearItem;
                gear.GetComponent<RespiratorCanister>().m_ProtectionDurationRTSeconds = 300;

                addedCustomComponents = true;
            }

        }
    }
}