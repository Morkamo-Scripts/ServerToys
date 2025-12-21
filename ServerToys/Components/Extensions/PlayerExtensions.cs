using Exiled.API.Features;
using ServerToys.Components.Features;

namespace ServerToys.Components.Extensions;

public static class PlayerExtensions
{
    public static PlayerServerToys PlayerServerToys(this Player player)
        => player.ReferenceHub.gameObject.GetComponent<PlayerServerToys>();
}