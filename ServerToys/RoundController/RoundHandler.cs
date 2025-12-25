using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using LabApi.Events.Arguments.ServerEvents;
using PlayerRoles;

namespace ServerToys.RoundController;

public class RoundHandler
{
    public void OnReceivingEffect(ReceivingEffectEventArgs ev)
    {
        if (Round.IsEnded)
        {
            if (ev.Intensity != 0)
                ev.IsAllowed = false;
        }
    }
    
    public void OnSpawned(SpawnedEventArgs ev)
    {
        if (ev.Player.Role.Type == RoleTypeId.Flamingo || ev.Player.Role.Type == RoleTypeId.ZombieFlamingo)
        {
            ev.Player.Health = 150;
            ev.Player.MaxHealth = 150;
        }
    }
}