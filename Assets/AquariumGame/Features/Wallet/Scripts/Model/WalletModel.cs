using System;
using AquariumGame.Features.Wallet.Scripts.Configs.Interface;
using AquariumGame.Features.Wallet.Scripts.Model.Interface;

namespace AquariumGame.Features.Wallet.Scripts.Model
{
    public sealed class WalletModel : IWalletModel
    {
        private readonly IWalletConfig _config;

        public int Coins { get; private set; }

        public event Action<int> Changed;

        public WalletModel(IWalletConfig config)
        {
            _config = config;
            Coins = config.StartCoins;
        }

        public void Reset() => Set(_config.StartCoins);

        public void Add(int amount)
        {
            if (amount <= 0)
                return;

            Set(Coins + amount);
        }

        public bool TrySpend(int amount)
        {
            if (amount < 0 || Coins < amount)
                return false;

            Set(Coins - amount);
            return true;
        }

        private void Set(int value)
        {
            Coins = value;
            Changed?.Invoke(value);
        }
    }
}
