using System;
using System.Linq;
using AdminToys;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.API.Features.Toys;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using HarmonyLib;
using InventorySystem.Items.Scp1509;
using LabApi.Events.Arguments.ServerEvents;
using MEC;
using RueI.API;
using RueI.API.Elements;
using ServerToys.Components;
using ServerToys.Components.Extensions;
using UnityEngine;
using Map = LabApi.Features.Wrappers.Map;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace ServerToys.Scp1509Capybara;

public class Variant1509Cp : CustomItem
{
    public override uint Id { get; set; } = 2;
    public override string Name { get; set; } = "SCP-1509-CP";
    public override string Description { get; set; } = "<b><color=red>SCP-1509-CP</color></b>";
    public override float Weight { get; set; } = 1;
    public override ItemType Type { get; set; } = ItemType.SCP1509;
    public override SpawnProperties SpawnProperties { get; set; } = null;

    protected override void SubscribeEvents()
    {
        Exiled.Events.Handlers.Player.Died += OnDied;
        Exiled.Events.Handlers.Player.ChangedItem += OnChangedItem;
        base.SubscribeEvents();
    }

    protected override void UnsubscribeEvents()
    {
        Exiled.Events.Handlers.Player.Died -= OnDied;
        Exiled.Events.Handlers.Player.ChangedItem -= OnChangedItem;
        base.UnsubscribeEvents();
    }

    private void OnChangedItem(ChangedItemEventArgs ev)
    {
        if (Check(ev.OldItem))
            Object.Destroy(ev.Player.PlayerServerToys().PlayerProps.CpParent);
        
        if (!Check(ev.Item))
            return;
            
        RueDisplay.Get(ev.Player).Show(
            new Tag(),
            new BasicElement(200, Description),
            1);

        Timing.CallDelayed(1.3f, () =>
        {
            RueDisplay.Get(ev.Player).Update();
        });

        var props = ev.Player.PlayerServerToys().PlayerProps;
        
        props.CpParent = new GameObject("cpParent")
        {
            transform =
            {
                position = ev.Player.Transform.position
            }
        };
        props.CpParent.transform.SetParent(ev.Player.Transform);
                            
        HighlightManager.ProceduralParticles(props.CpParent, Color.white, 0, 0.05f,
            new(3f, 3f, 3f), 0.1f, 15);
    }

    protected override void OnDroppingItem(DroppingItemEventArgs ev)
    {
        if (Check(ev.Item))
            ev.Item.Destroy();
        base.OnDroppingItem(ev);
    }

    /*if (Check(ev.Attacker.CurrentItem))
    {
        var capybara = PrefabHelper.Spawn(PrefabType.CapybaraToy, ev.Ragdoll.Position);
        capybara.SetWorldScale(new Vector3(3, 3, 3));
        capybara.transform.SetParent(ev.Ragdoll.Transform);
            
        var light = HighlightManager.MakeLight(capybara.transform.position, Color.magenta, LightShadows.None);
        HighlightManager.ProceduralParticles(capybara, Color.magenta, 0, 0.05f,
            new(1f, 1f, 1f), 0.1f, 15);

        Timing.CallDelayed(5, () =>
        {
            Object.Destroy(capybara);
            Object.Destroy(light.GameObject);
        });
    }*/
        
    private void OnDied(DiedEventArgs ev)
    {
        if (ev.Attacker.CurrentItem != null && Check(ev.Attacker?.CurrentItem))
        {
            var center = ev.Ragdoll.Position + Vector3.up * 0.3f;
            var radius = 1.2f;
            var count = 8;

            for (int i = 0; i < count; i++)
            {
                var offset = Random.insideUnitSphere * radius;
                offset.y = 0f;

                var spawnPos = center + offset + Vector3.up * 2f;

                var rotation = Quaternion.Euler(
                    0f,
                    Random.Range(0f, 360f),
                    0f
                );

                var capybara = PrefabHelper.Spawn(PrefabType.CapybaraToy, spawnPos, rotation);
                capybara.SetWorldScale(new Vector3(0.6f, 0.6f, 0.6f));

                var rb = capybara.AddComponent<Rigidbody>();
                rb.useGravity = true;
                rb.isKinematic = false;
                rb.mass = 2f;
                rb.interpolation = RigidbodyInterpolation.Interpolate;
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

                if (capybara.GetComponent<Collider>() == null)
                {
                    var collider = capybara.AddComponent<BoxCollider>();
                    collider.size = Vector3.one;
                }

                /*var light = HighlightManager.MakeLight(capybara.transform.position, Color.magenta, LightShadows.None);
                HighlightManager.ProceduralParticles(capybara, Color.magenta, 0, 0.05f,
                    new(1f, 1f, 1f), 0.1f, 15);

                Timing.CallDelayed(5, () =>
                {
                    if (capybara != null)
                        Object.Destroy(capybara);

                    if (light != null)
                        Object.Destroy(light.GameObject);
                });*/
            }
        }
    }
}