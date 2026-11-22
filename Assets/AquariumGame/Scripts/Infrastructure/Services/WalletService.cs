using System;

namespace AquariumGame.Infrastructure.Services
{
    public sealed class WalletService : IWalletService
    {
        public const int StartCoins = 300;

        public int Coins { get; private set; } = StartCoins;

        public event Action<int> Changed;

        public void Reset() => Set(StartCoins);

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
