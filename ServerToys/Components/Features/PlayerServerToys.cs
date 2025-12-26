using Exiled.API.Features;
using ServerToys.Components.Features.Components;
using UnityEngine;

namespace ServerToys.Components.Features;

public sealed class PlayerServerToys() : MonoBehaviour
{
    private void Awake()
    {
        Player = Player.Get(gameObject);
        CoinProps = new CoinProps(this);
        PlayerProps = new PlayerProps(this);
    }

    public Player Player { get; private set; }
    public CoinProps CoinProps { get; private set; }
    public PlayerProps PlayerProps { get; private set; }
}