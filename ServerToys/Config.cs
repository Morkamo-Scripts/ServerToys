using Exiled.API.Interfaces;
using ServerToys.ReworkedCoin;

namespace ServerToys
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        
        public CoinHandler Handler { get; set; } = new CoinHandler();
        public bool IsLightflickerEnabled { get; set; } = true;
    }
}