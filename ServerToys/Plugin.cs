using System;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.CustomItems.API;
using Exiled.Events.EventArgs.Player;
using HarmonyLib;
using InventorySystem.Items.Coin;
using InventorySystem.Items.Scp1509;
using MEC;
using Mirror;
using PlayerRoles;
using ServerToys.Components.Events;
using ServerToys.Components.Extensions;
using ServerToys.Components.Features;
using ServerToys.Lightflicker;
using ServerToys.ReworkedCoin;
using ServerToys.RoundController;
using ServerToys.Scp1509InventoryReset;
using ServerToys.Scp2356Upgraded;
using events = Exiled.Events.Handlers;

namespace ServerToys
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "ServerToys";
        public override string Prefix => Name;
        public override string Author => "Morkamo";
        public override Version RequiredExiledVersion => new(9, 1, 0);
        public override Version Version => new(2, 5, 0);

        public static Plugin Instance;
        public static Harmony Harmony;
        public LightflickerHandler LightflickerHandler;
        public RoundHandler RoundHandler;
        public Scp2356UpgradeHandler Scp2356UpgradeHandler;
        public Scp1509InventoryHandler Scp1509InventoryHandler;

        public override void OnEnabled()
        {
            Instance = this;
            
            Harmony = new Harmony("ru.morkamo.serverToys.patches");
            Harmony.PatchAll();
            
            LightflickerHandler = new LightflickerHandler();
            RoundHandler =  new RoundHandler();
            Scp2356UpgradeHandler = new Scp2356UpgradeHandler();
            Scp1509InventoryHandler = new Scp1509InventoryHandler();
            
            Config.Scp1509Capybara.Register();
            
            RegisterEvents();
            base.OnEnabled();
        }

        public override void OnDisabled() 
        {
            UnregisterEvents();

            RoundHandler = null;
            LightflickerHandler = null;
            Scp2356UpgradeHandler = null;
            Scp1509InventoryHandler = null;
            
            Config.Scp1509Capybara.Unregister();
            
            Harmony.UnpatchAll();
            Instance = null;
            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            events.Player.Verified += OnVerifiedPlayer;
            /*events.Player.FlippingCoin += CoinHandler.OnCoinFlipped;*/
            events.Server.RoundStarted += LightflickerHandler.OnRoundStarted;
            /*events.Player.ChangingRole += CoinHandler.OnChangingRole;*/
            LabApi.Events.Handlers.PlayerEvents.Spawned += RoundHandler.OnSpawned;
            /*events.Player.RemovedHandcuffs += CoinHandler.OnRemovedHandcuffs;*/
            events.Player.Hurting += RoundHandler.OnHurting;
            events.Server.RoundEnded += RoundHandler.OnRoundEnded;
            events.Server.WaitingForPlayers += RoundHandler.OnWaitForPlayers;
            events.Player.ReceivingEffect += RoundHandler.OnReceivingEffect;
            events.Scp2536.GrantingGift += Scp2356UpgradeHandler.OnGrantingGift;
            events.Player.Spawned += Scp1509InventoryHandler.OnPlayerResurrected;
            LabApi.Events.Handlers.ServerEvents.CassieAnnouncing += LightflickerHandler.OnCassieAnnouncing;
            LabApi.Events.Handlers.ServerEvents.RoundEnded += LightflickerHandler.OnRoundEnded;
            EventManager.PlayerEvents.PlayerFullConnected += RoundHandler.OnPlayerFullConnected;
        }

        private void UnregisterEvents()
        {
            events.Player.Verified -= OnVerifiedPlayer;
            /*events.Player.FlippingCoin -= CoinHandler.OnCoinFlipped;*/
            events.Server.RoundStarted -= LightflickerHandler.OnRoundStarted;
            /*events.Player.ChangingRole -= CoinHandler.OnChangingRole;*/
            LabApi.Events.Handlers.PlayerEvents.Spawned -= RoundHandler.OnSpawned;
            /*events.Player.RemovedHandcuffs -= CoinHandler.OnRemovedHandcuffs;*/
            events.Player.Hurting -= RoundHandler.OnHurting;
            events.Server.RoundEnded -= RoundHandler.OnRoundEnded;
            events.Server.WaitingForPlayers -= RoundHandler.OnWaitForPlayers;
            events.Player.ReceivingEffect -= RoundHandler.OnReceivingEffect;
            events.Scp2536.GrantingGift -= Scp2356UpgradeHandler.OnGrantingGift;
            events.Player.Spawned -= Scp1509InventoryHandler.OnPlayerResurrected;
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