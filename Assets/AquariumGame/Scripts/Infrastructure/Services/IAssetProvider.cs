using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AquariumGame.Infrastructure.Services
{
    public interface IAssetProvider
    {
        UniTask InitializeAsync(CancellationToken token);
        UniTask UpdateCatalogsAsync(CancellationToken token);
        UniTask PreloadByLabelAsync(string label, CancellationToken token);
        UniTask<T> LoadAsync<T>(string key, CancellationToken token) where T : Object;
        void ReleaseAll();
    }
}
