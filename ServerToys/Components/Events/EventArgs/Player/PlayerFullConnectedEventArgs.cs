namespace ServerToys.Components.Events.EventArgs.Player
{
    using Exiled.API.Features;
    
    public class PlayerFullConnectedEventArgs(Player player)
    {
        public Player Player => player;
    }
}