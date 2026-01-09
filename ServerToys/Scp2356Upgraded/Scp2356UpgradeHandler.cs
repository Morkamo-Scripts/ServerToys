using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Scp2536;
using MEC;
using ProjectMER.Events.Handlers;
using ProjectMER.Features.Objects;
using UnityEngine;

namespace ServerToys.Scp2356Upgraded;

public class Scp2356UpgradeHandler
{
    private static List<SchematicObject> SpawnedMelons { get; set; } = new();
    /*private static List<SchematicObject> SpawnedMiyabies { get; set; } = new();*/
    
    private const int MaxMiyabi = 5;
    private const int MaxMelon = 10;
    
    public void OnGrantingGift(GrantingGiftEventArgs ev)
    {
        var forwardOffset = 1.2f;
        var spawnPos = ev.Player.Transform.position + ev.Player.Transform.forward * forwardOffset;
        SchematicObject toy;

        if (ev.Player.Zone != ZoneType.Surface && Random.Range(1, 101) <= 20)
        {
            if (!ProjectMER.Features.MapUtils.TryGetSchematicDataByName("MiyabiToy", out _))
                return;

            /*if (SpawnedMiyabies.Count >= MaxMiyabi)
            {
                var oldest = SpawnedMiyabies[0];
                SpawnedMiyabies.RemoveAt(0);

                if (oldest != null)
                {
                    try { oldest.Destroy(); }
                    catch { /* ignored #1# }
                }
            }*/

            toy = ProjectMER.Features.ObjectSpawner.SpawnSchematic("MiyabiToy", spawnPos);
            Timing.CallDelayed(120f, () => toy.Destroy());
        }
        else if (Random.Range(1, 101) <= 30)
        {
            if (!ProjectMER.Features.MapUtils.TryGetSchematicDataByName("MelonToy", out _))
                return;

            if (SpawnedMelons.Count >= MaxMelon)
            {
                var oldest = SpawnedMelons[0];
                SpawnedMelons.RemoveAt(0);

                if (oldest != null)
                {
                    try { oldest.Destroy(); }
                    catch { /* ignored */ }
                }
            }

            toy = ProjectMER.Features.ObjectSpawner.SpawnSchematic("MelonToy", spawnPos);
            SpawnedMelons.Add(toy);
        }
    }
}