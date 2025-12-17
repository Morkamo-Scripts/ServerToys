using Exiled.API.Features;
using LabApi.Events;
using ServerToys.Features.Components;
using ServerToys.ReworkedCoin;
using UnityEngine;

namespace ServerToys.Features;

public sealed class PlayerServerToys() : MonoBehaviour
{
    private void Awake()
    {
        Player = Player.Get(gameObject);
        CoinProps = new CoinProps(this);
    }

    public Player Player { get; private set; }
    public CoinProps CoinProps { get; private set; }
}