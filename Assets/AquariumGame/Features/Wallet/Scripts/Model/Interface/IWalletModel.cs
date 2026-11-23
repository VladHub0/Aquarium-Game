using System;

namespace AquariumGame.Features.Wallet.Scripts.Model.Interface
{
    public interface IWalletModel
    {
        int Coins { get; }

        event Action<int> Changed;

        void Reset();

        void Add(int amount);

        bool TrySpend(int amount);
    }
}
