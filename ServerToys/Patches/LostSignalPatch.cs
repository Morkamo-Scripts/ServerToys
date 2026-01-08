using HarmonyLib;
using PlayerRoles.PlayableScps.Scp079;

namespace ServerToys.Patches;

[HarmonyPatch(typeof(Scp079LostSignalHandler), nameof(Scp079LostSignalHandler.ServerLoseSignal))]
public class LostSignalPatch
{
    private static bool Prefix(ref float duration)
    {
        duration = Plugin.Instance.Config.LostSignalScp079Time;
        return true;
    }
}