using System.Threading;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Infrastructure.Services
{
    public interface IGameplaySession
    {
        bool IsActive { get; }

        UniTask StartAsync(CancellationToken token);

        UniTask SpawnFishAsync(string speciesKey, CancellationToken token);

        void End();
    }
}
