using System.Threading;
using AquariumGame.Features.Fish.Scripts.Catalog;
using AquariumGame.Features.Gameplay.Scripts.Services.Interface;
using AquariumGame.Features.Shop.Scripts.Services.Interface;
using AquariumGame.Features.Wallet.Scripts.Model.Interface;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Features.Shop.Scripts.Services
{
    public sealed class FishShop : IFishShop
    {
        private readonly IWalletModel _wallet;
        private readonly IGameplaySession _session;

        public FishShop(IWalletModel wallet, IGameplaySession session)
        {
            _wallet = wallet;
            _session = session;
        }

        public bool CanAfford(FishSpecies species) => _wallet.Coins >= species.Price;

        public async UniTask<bool> TryBuyAsync(FishSpecies species, CancellationToken token)
        {
            if (!_session.IsActive || !_wallet.TrySpend(species.Price))
                return false;

            try
            {
                await _session.SpawnFishAsync(species.Key, token);
                return true;
            }
            catch
            {
                _wallet.Add(species.Price);
                throw;
            }
        }
    }
}
