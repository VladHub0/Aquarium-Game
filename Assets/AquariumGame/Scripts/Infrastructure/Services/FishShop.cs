using System.Threading;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Infrastructure.Services
{
    public sealed class FishShop : IFishShop
    {
        private readonly IWalletService _wallet;
        private readonly IGameplaySession _session;

        public FishShop(IWalletService wallet, IGameplaySession session)
        {
            _wallet = wallet;
            _session = session;
        }

        public bool CanBuy(FishSpecies species) => _session.IsActive && _wallet.Coins >= species.Price;

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
