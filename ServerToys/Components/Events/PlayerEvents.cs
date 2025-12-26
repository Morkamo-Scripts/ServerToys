using System;
using Exiled.API.Features;
using LabApi.Events.Arguments.PlayerEvents;
using ServerToys.Components.Events.EventArgs.Player;

namespace ServerToys.Components.Events
{
    public partial class PlayerEvents
    {
        public event Action<PlayerFullConnectedEventArgs> PlayerFullConnected;
    }

    public partial class PlayerEvents
    {
        public void InvokePlayerFullConnected(Player player)
        {
            var ev = new PlayerFullConnectedEventArgs(player);
            PlayerFullConnected?.Invoke(ev);
        }
    }
}