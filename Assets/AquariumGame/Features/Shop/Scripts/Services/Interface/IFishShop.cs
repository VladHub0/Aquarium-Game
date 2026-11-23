using System.Threading;
using AquariumGame.Features.Fish.Scripts.Catalog;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Features.Shop.Scripts.Services.Interface
{
    public interface IFishShop
    {
        bool CanAfford(FishSpecies species);

        UniTask<bool> TryBuyAsync(FishSpecies species, CancellationToken token);
    }
}
