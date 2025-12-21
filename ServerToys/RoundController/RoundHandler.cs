using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using LabApi.Events.Arguments.ServerEvents;

namespace ServerToys.RoundController;

public class RoundHandler
{
    public void OnReceivingEffect(ReceivingEffectEventArgs ev)
    {
        if (Round.IsEnded)
        {
            if (ev.Intensity != 0)
                ev.IsAllowed = false;
        }
    }
}