using System.Threading;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Infrastructure.Services
{
    public interface ISceneLoader
    {
        /// <summary>
        /// Загружает сцену аддитивно поверх Bootstrap и выгружает предыдущую.
        /// </summary>
        UniTask LoadAsync(string sceneKey, CancellationToken token);
    }
}
