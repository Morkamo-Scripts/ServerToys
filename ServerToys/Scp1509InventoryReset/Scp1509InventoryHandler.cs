using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Scp1509;
using LabApi.Events.Arguments.PlayerEvents;
using PlayerRoles;

namespace ServerToys.Scp1509InventoryReset;

public class Scp1509InventoryHandler
{
    public void OnPlayerResurrected(SpawnedEventArgs ev)
    {
        if (ev.Reason == SpawnReason.Resurrected)
            ev.Player.Role.Set(ev.Player.Role, SpawnReason.None, RoleSpawnFlags.AssignInventory);
    }
}