using System.Threading;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Infrastructure.Services
{
    public interface IFishShop
    {
        bool CanBuy(FishSpecies species);

        UniTask<bool> TryBuyAsync(FishSpecies species, CancellationToken token);
    }
}
