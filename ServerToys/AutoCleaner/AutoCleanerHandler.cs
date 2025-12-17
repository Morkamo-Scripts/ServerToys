using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using Exiled.Events.EventArgs.Map;
using LabApi.Events.Arguments.WarheadEvents;
using MapGeneration;

namespace ServerToys.AutoCleaner;

public class AutoCleanerHandler
{
    public void OnDecontaminatedLcz(DecontaminatingEventArgs ev)
    {
        Map.CleanAllItems(Pickup.List.Where(p => p.Room != null && p.Room.Zone == ZoneType.LightContainment).ToList());
        Map.CleanAllRagdolls(Ragdoll.List.Where(rg => rg.Zone == ZoneType.LightContainment).ToList());
    }

    public void OnWarheadDetonated()
    {
        Map.CleanAllItems(
            Pickup.List
                .Where(p =>
                    p.Room != null &&
                    (
                        p.Room.Zone == ZoneType.LightContainment ||
                        p.Room.Zone == ZoneType.HeavyContainment ||
                        p.Room.Zone == ZoneType.Entrance
                    )
                )
                .ToList()
        );
        
        Map.CleanAllRagdolls(
            Ragdoll.List
                .Where(p =>
                    p.Room != null &&
                    (
                        p.Room.Zone == ZoneType.LightContainment ||
                        p.Room.Zone == ZoneType.HeavyContainment ||
                        p.Room.Zone == ZoneType.Entrance
                    )
                )
                .ToList()
        );
    }
}