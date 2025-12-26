using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using MEC;
using PlayerRoles;
using ServerToys.Components.Events.EventArgs.Player;
using ServerToys.Components.Extensions;
using RoundEndedEventArgs = Exiled.Events.EventArgs.Server.RoundEndedEventArgs;

namespace ServerToys.RoundController;

public class RoundHandler
{
    private readonly HashSet<RoleTypeId> _rolesForSpawn =
    [
        RoleTypeId.ClassD,
        RoleTypeId.Scientist,
        RoleTypeId.FacilityGuard
    ];

    private HashSet<string> _earlySpawnedPlayer = new();
    
    public void OnSpawned(PlayerSpawnedEventArgs ev)
    {
        if (ev.Player.Role == RoleTypeId.Flamingo || ev.Player.Role == RoleTypeId.ZombieFlamingo)
        {
            ev.Player.Health = 150;
            ev.Player.MaxHealth = 150;
        }
        
        _earlySpawnedPlayer.Add(ev.Player.UserId);
    }

    public void OnReceivingEffect(ReceivingEffectEventArgs ev)
    {
        if (Round.IsEnded)
        {
            if (ev.Effect.GetEffectType() == EffectType.SpawnProtected && ev.Intensity != 0)
                ev.IsAllowed = false;
        }
    }
    
    // Фикс зацикленного урона от поезда.
    public void OnHurting(HurtingEventArgs ev)
    {
        Log.Info($"Protected: {ev.DamageHandler.Type}");
        
        if (ev.DamageHandler.Type == DamageType.Crushed)
            ev.Player.IsSpawnProtected = false;
    }
    
    // Распределение позднозашедших игроков.
    public void OnPlayerFullConnected(PlayerFullConnectedEventArgs ev)
    {
        if (Round.ElapsedTime.TotalSeconds <= 90 && Round.IsStarted && !Round.IsEnded 
            && !_earlySpawnedPlayer.Contains(ev.Player.UserId))
        {
            ev.Player.Role.Set(_rolesForSpawn.GetRandomValue(), SpawnReason.RoundStart);
            _earlySpawnedPlayer.Add(ev.Player.UserId);
        }
    }

    public void OnWaitForPlayers()
    {
        _earlySpawnedPlayer.Clear();
    }
    
    public void OnRoundEnded(RoundEndedEventArgs ev)
    {
        _earlySpawnedPlayer.Clear();
    }
}