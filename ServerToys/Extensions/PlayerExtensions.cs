using Exiled.API.Features;
using ServerToys.Features;

namespace ServerToys.Extensions;

public static class PlayerExtensions
{
    public static PlayerServerToys PlayerServerToys(this Player player)
        => player.GameObject.GetComponent<PlayerServerToys>();
    
    public static PlayerServerToys PlayerServerToys(this LabApi.Features.Wrappers.Player player)
        => player.GameObject!.GetComponent<PlayerServerToys>();
}