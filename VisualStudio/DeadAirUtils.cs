﻿using UnityEngine.AddressableAssets;
using UnityEngine;
using Il2Cpp;
using MelonLoader;

namespace DeadAir
{
    internal static class DeadAirUtils
    {
        public static Panel_Inventory inventory;
        public static GearItem canister = Addressables.LoadAssetAsync<GameObject>("GEAR_Canister").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem canister2 = Addressables.LoadAssetAsync<GameObject>("GEAR_Canister").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem canister3 = Addressables.LoadAssetAsync<GameObject>("GEAR_Canister").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem canisterimprov = Addressables.LoadAssetAsync<GameObject>("GEAR_ImprovisedFilter").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem respirator = Addressables.LoadAssetAsync<GameObject>("GEAR_Respirator").WaitForCompletion().GetComponent<GearItem>();

        public static GameObject GetPlayer()
        {
            return GameManager.GetPlayerObject();
        }

        public static T? GetComponentSafe<T>(this Component? component) where T : Component
        {
            return component == null ? default : GetComponentSafe<T>(component.GetGameObject());
        }
        public static T? GetComponentSafe<T>(this GameObject? gameObject) where T : Component
        {
            return gameObject == null ? default : gameObject.GetComponent<T>();
        }
        public static T? GetOrCreateComponent<T>(this Component? component) where T : Component
        {
            return component == null ? default : GetOrCreateComponent<T>(component.GetGameObject());
        }
        public static T? GetOrCreateComponent<T>(this GameObject? gameObject) where T : Component
        {
            if (gameObject == null)
            {
                return default;
            }

            T? result = GetComponentSafe<T>(gameObject);

            if (result == null)
            {
                result = gameObject.AddComponent<T>();
            }

            return result;
        }
        internal static GameObject? GetGameObject(this Component? component)
        {
            try
            {
                return component == null ? default : component.gameObject;
            }
            catch (System.Exception exception)
            {
                MelonLoader.MelonLogger.Msg($"Returning null since this could not obtain a Game Object from the component. Stack trace:\n{exception.Message}");
            }
            return null;
        }

        public static bool IsScenePlayable()
        {
            return !(string.IsNullOrEmpty(GameManager.m_ActiveScene) || GameManager.m_ActiveScene.Contains("MainMenu") || GameManager.m_ActiveScene == "Boot" || GameManager.m_ActiveScene == "Empty");
        }

        public static bool IsScenePlayable(string scene)
        {
            return !(string.IsNullOrEmpty(scene) || scene.Contains("MainMenu") || scene == "Boot" || scene == "Empty");
        }

        public static bool IsMainMenu(string scene)
        {
            return !string.IsNullOrEmpty(scene) && scene.Contains("MainMenu");
        }
        public static bool IsSceneSafe(string scene)
        {
            return !string.IsNullOrEmpty(scene) && scene.Contains("Prepper");
        }
        public static bool IsSceneHouse(string scene)
        {
            return !string.IsNullOrEmpty(scene) && scene.Contains("Cabin") || scene.Contains("House") || scene.Contains("Church");
        }
        public static bool IsSceneCinderHills(string scene)
        {
            return !string.IsNullOrEmpty(scene) && scene.Contains("MineTransitionZone");
        }

    }

}
