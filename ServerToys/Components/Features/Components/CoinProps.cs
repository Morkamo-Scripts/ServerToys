using System.Collections.Generic;
using ServerToys.Components.Features.Components.Interfaces;
using UnityEngine;

namespace ServerToys.Components.Features.Components;

public class CoinProps(PlayerServerToys playerServerToys) : IPlayerPropertyModule
{
    public PlayerServerToys PlayerServerToys { get; } = playerServerToys;

    public bool IsCoinZombie { get; set; } = false;

    public Dictionary<string, Coroutine> ActiveCustomEffects { get; set; } = new();
    
    public GameObject ZombieHightlighterParent { get; set; }
}