using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Godmode
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        static ManualLogSource logger;

        private void Awake()
        {
            // Plugin startup logic
            logger = Logger;
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded");
            Logger.LogInfo($"Patching...");
            Harmony.CreateAndPatchAll(typeof(Plugin));
            Logger.LogInfo($"Patched");
        }

        [HarmonyPatch(typeof(StatOSD), "Update")]
        [HarmonyPostfix]
        static void Update_Postfix(StatOSD __instance)
        {
            __instance.oldRunned = 0f;
            __instance.oldwalked = 0f;
            __instance.health = 1f;
            __instance.hunger = 1f;
            __instance.thirst = 1f;
            __instance.tiredness = 0f;
            __instance.stamina = 1f;
            __instance.wet = 0f;
            __instance.heat = __instance.heatNormal;
        }

        [HarmonyPatch(typeof(MainMenuScreen), "ShowDeathScreen")]
        [HarmonyPrefix]
        static bool ShowDeathScreen_Prefix()
        {
            return false;
        }
    }
}
