using System.Threading;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Features.Gameplay.Scripts.Services.Interface
{
    public interface IGameplaySession
    {
        bool IsActive { get; }

        UniTask StartAsync(CancellationToken token);

        UniTask SpawnFishAsync(string speciesKey, CancellationToken token);

        void End();
    }
}
