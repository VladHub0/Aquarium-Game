using System;

namespace AquariumGame.Infrastructure.Services
{
    public interface IWalletService
    {
        int Coins { get; }

        event Action<int> Changed;

        void Reset();

        void Add(int amount);

        bool TrySpend(int amount);
    }
}
