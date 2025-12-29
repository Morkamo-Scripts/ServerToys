using System;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.CustomItems.API;
using Exiled.Events.EventArgs.Player;
using HarmonyLib;
using InventorySystem.Items.Coin;
using MEC;
using Mirror;
using PlayerRoles;
using ServerToys.Components.Events;
using ServerToys.Components.Extensions;
using ServerToys.Components.Features;
using ServerToys.Lightflicker;
using ServerToys.ReworkedCoin;
using ServerToys.RoundController;
using events = Exiled.Events.Handlers;

namespace ServerToys
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "ServerToys";
        public override string Prefix => Name;
        public override string Author => "Morkamo";
        public override Version RequiredExiledVersion => new(9, 1, 0);
        public override Version Version => new(2, 2, 0);

        public static Plugin Instance;
        public static Harmony Harmony;
        public CoinHandler CoinHandler;
        public LightflickerHandler LightflickerHandler;
        /*public AutoCleanerHandler AutoCleanerHandler;*/
        public RoundHandler RoundHandler;

        public override void OnEnabled()
        {
            Instance = this;
            
            Harmony = new Harmony("ru.morkamo.serverToys.patches");
            Harmony.PatchAll();
            
            CoinHandler = Config.Handler;
            LightflickerHandler = new LightflickerHandler();
            RoundHandler =  new RoundHandler();
            
            Config.Scp1509Capybara.Register();
            
            RegisterEvents();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();

            RoundHandler = null;
            LightflickerHandler = null;
            CoinHandler = null;
            
            Config.Scp1509Capybara.Unregister();
            
            Harmony.UnpatchAll();
            Instance = null;
            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            events.Player.Verified += OnVerifiedPlayer;
            events.Player.FlippingCoin += CoinHandler.OnCoinFlipped;
            events.Server.RoundStarted += LightflickerHandler.OnRoundStarted;
            events.Player.ChangingRole += CoinHandler.OnChangingRole;
            LabApi.Events.Handlers.PlayerEvents.Spawned += RoundHandler.OnSpawned;
            events.Player.RemovedHandcuffs += CoinHandler.OnRemovedHandcuffs;
            events.Player.Hurting += RoundHandler.OnHurting;
            events.Server.RoundEnded += RoundHandler.OnRoundEnded;
            events.Server.WaitingForPlayers += RoundHandler.OnWaitForPlayers;
            events.Player.ReceivingEffect += RoundHandler.OnReceivingEffect;
            LabApi.Events.Handlers.ServerEvents.CassieAnnouncing += LightflickerHandler.OnCassieAnnouncing;
            LabApi.Events.Handlers.ServerEvents.RoundEnded += LightflickerHandler.OnRoundEnded;
            EventManager.PlayerEvents.PlayerFullConnected += RoundHandler.OnPlayerFullConnected;
        }

        private void UnregisterEvents()
        {
            events.Player.Verified -= OnVerifiedPlayer;
            events.Player.FlippingCoin -= CoinHandler.OnCoinFlipped;
            events.Server.RoundStarted -= LightflickerHandler.OnRoundStarted;
            events.Player.ChangingRole -= CoinHandler.OnChangingRole;
            LabApi.Events.Handlers.PlayerEvents.Spawned -= RoundHandler.OnSpawned;
            events.Player.RemovedHandcuffs -= CoinHandler.OnRemovedHandcuffs;
            events.Player.Hurting -= RoundHandler.OnHurting;
            events.Server.RoundEnded -= RoundHandler.OnRoundEnded;
            events.Server.WaitingForPlayers -= RoundHandler.OnWaitForPlayers;
            events.Player.ReceivingEffect -= RoundHandler.OnReceivingEffect;
            LabApi.Events.Handlers.ServerEvents.CassieAnnouncing -= LightflickerHandler.OnCassieAnnouncing;
            LabApi.Events.Handlers.ServerEvents.RoundEnded -= LightflickerHandler.OnRoundEnded;
            EventManager.PlayerEvents.PlayerFullConnected -= RoundHandler.OnPlayerFullConnected;
        }

        private void OnVerifiedPlayer(VerifiedEventArgs ev)
        {
            if (ev.Player.ReferenceHub.gameObject.GetComponent<PlayerServerToys>() != null)
                return;

            ev.Player.ReferenceHub.gameObject.AddComponent<PlayerServerToys>();
            
            EventManager.PlayerEvents.InvokePlayerFullConnected(ev.Player);
        }
    }
}