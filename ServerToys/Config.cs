using Exiled.API.Interfaces;
using ServerToys.ReworkedCoin;
using ServerToys.Scp1509Capybara;

namespace ServerToys
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        
        public CoinHandler Handler { get; set; } = new CoinHandler();
        public bool IsLightflickerEnabled { get; set; } = true;
        
        public Variant1509Cp Scp1509Capybara { get; set; } = new Variant1509Cp();
    }
}