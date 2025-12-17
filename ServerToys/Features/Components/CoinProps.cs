using System.Collections.Generic;
using ServerToys.Features.Components.Interfaces;
using UnityEngine;

namespace ServerToys.Features.Components;

public class CoinProps(PlayerServerToys playerServerToys) : IPlayerPropertyModule
{
    public PlayerServerToys PlayerServerToys { get; } = playerServerToys;

    public bool IsZombieCoinHealing { get; set; } = false;

    public Dictionary<string, Coroutine> ActiveCustomEffects { get; set; } = new();
    
    public GameObject ZombieHightlighterParent { get; set; }
}