using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace ServerToys.RoundController;

public class RoundHandler
{
    public void OnReceivingEffect(ReceivingEffectEventArgs ev)
    {
        if (!Round.IsEnded)
            return;

        if (ev.Intensity != 0)
            ev.IsAllowed = false;
    }
}