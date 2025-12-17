using System.Reflection;
using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using HarmonyLib;
using InventorySystem.Items.Jailbird;

namespace ServerToys.Patches
{
    /*[HarmonyPatch(typeof(JailbirdDeteriorationTracker), "RecheckUsage")]
    public class JailbirdWearStatePatchь
    {
        private static bool Prefix(JailbirdDeteriorationTracker __instance)
        {
            var field = typeof(JailbirdDeteriorationTracker).GetField("_jailbird", BindingFlags.NonPublic | BindingFlags.Instance);
            var jailbird = field?.GetValue(__instance) as JailbirdItem;
            
            if (jailbird)
            {
                var owner = Player.Get(jailbird.Owner.networkIdentity);
                if (owner != null && owner.IsAlive)
                    return false;
            }
            return true;
        }
    }*/
}