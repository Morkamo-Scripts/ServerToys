using System.Collections.Generic;
using ServerToys.Components.Features.Components.Interfaces;
using UnityEngine;

namespace ServerToys.Components.Features.Components;

public class PlayerProps(PlayerServerToys playerServerToys) : IPlayerPropertyModule
{
    public PlayerServerToys PlayerServerToys { get; } = playerServerToys;
    
    public bool IsCrushDamageProtectectEnabled { get; set; } = false;
}