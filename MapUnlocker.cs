using Assets.Scripts.Data;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.UI_New.Main;
using HarmonyLib;
using MelonLoader;

namespace Btd6MapUnlocker
{
    public class MapUnlocker : MelonMod
    {
        private static bool HasEnabledThisSession;
        
        public override void OnApplicationStart()
        {
            MelonLogger.Msg("BTD6 Map Unlocker Loaded");
        }

        [HarmonyPatch(typeof(MainMenu), nameof(MainMenu.OnEnable))]
        public class MainMenuEnable_Patch
        {
            [HarmonyPostfix]
            [HarmonyPriority(HarmonyLib.Priority.Last)]
            public static void Postfix()
            {
                if(HasEnabledThisSession)
                    return;

                HasEnabledThisSession = true;

                MelonLogger.Msg("Unlocking all maps now...");

                var player = Game.instance.playerService.Player;
                
                foreach (var mapDetails in GameData._instance.mapSet.Maps.items)
                {
                    MelonLogger.Msg($"Unlocking {mapDetails.id}...");
                    player.UnlockMap(mapDetails.id, false);
                }
            }
        }
    }
}