using System.Threading;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Infrastructure.Services
{
    public interface ISceneLoader
    {
        UniTask LoadAsync(string sceneKey, CancellationToken token);

        UniTask LoadOverlayAsync(string sceneKey, CancellationToken token);

        UniTask UnloadOverlayAsync(CancellationToken token);
    }
}
