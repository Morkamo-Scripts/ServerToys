using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using HarmonyLib;
using ServerToys.AutoCleaner;
using ServerToys.Features;
using ServerToys.Lightflicker;
using ServerToys.ReworkedCoin;
using events = Exiled.Events.Handlers;

namespace ServerToys
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "ServerToys";
        public override string Prefix => Name;
        public override string Author => "Morkamo";
        public override Version RequiredExiledVersion => new(9, 1, 0);
        public override Version Version => new(1, 0, 0);

        public static Plugin Instance;
        public static Harmony Harmony;
        public CoinHandler CoinHandler;
        public LightflickerHandler LightflickerHandler;
        public AutoCleanerHandler AutoCleanerHandler;

        public override void OnEnabled()
        {
            Instance = this;
            
            Harmony = new Harmony("ru.morkamo.patches");
            Harmony.PatchAll();
            
            CoinHandler = Config.Handler;
            LightflickerHandler = new LightflickerHandler();
            AutoCleanerHandler = new AutoCleanerHandler();
            
            RegisterEvents();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();
            
            AutoCleanerHandler = null;
            LightflickerHandler = null;
            CoinHandler = null;
            
            Harmony.UnpatchAll();
            Instance = null;
            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            events.Player.Verified += OnVerifiedPlayer;
            events.Player.Died += CoinHandler.OnDied;
            events.Player.FlippingCoin += CoinHandler.OnCoinFlipped;
            events.Server.RoundStarted += LightflickerHandler.OnRoundStarted;
            events.Map.Decontaminating += AutoCleanerHandler.OnDecontaminatedLcz;
            events.Warhead.Detonated += AutoCleanerHandler.OnWarheadDetonated;
            LabApi.Events.Handlers.PlayerEvents.ChangedRole += CoinHandler.OnChangedRole;
            LabApi.Events.Handlers.ServerEvents.CassieAnnouncing += LightflickerHandler.OnCassieAnnouncing;
            LabApi.Events.Handlers.ServerEvents.RoundEnded += LightflickerHandler.OnRoundEnded;
        }

        private void UnregisterEvents()
        {
            events.Player.Verified -= OnVerifiedPlayer;
            events.Player.Died -= CoinHandler.OnDied;
            events.Player.FlippingCoin -= CoinHandler.OnCoinFlipped;
            events.Server.RoundStarted -= LightflickerHandler.OnRoundStarted;
            events.Map.Decontaminating -= AutoCleanerHandler.OnDecontaminatedLcz;
            events.Warhead.Detonated -= AutoCleanerHandler.OnWarheadDetonated;
            LabApi.Events.Handlers.PlayerEvents.ChangedRole -= CoinHandler.OnChangedRole;
            LabApi.Events.Handlers.ServerEvents.CassieAnnouncing -= LightflickerHandler.OnCassieAnnouncing;
            LabApi.Events.Handlers.ServerEvents.RoundEnded -= LightflickerHandler.OnRoundEnded;
        }

        private void OnVerifiedPlayer(VerifiedEventArgs ev)
        {
            if (ev.Player.ReferenceHub.gameObject.GetComponent<PlayerServerToys>() != null)
                return;

            ev.Player.ReferenceHub.gameObject.AddComponent<PlayerServerToys>();
        }
    }
}