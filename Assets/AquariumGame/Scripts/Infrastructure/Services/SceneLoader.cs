using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Zenject;

namespace AquariumGame.Infrastructure.Services
{
    public sealed class SceneLoader : ISceneLoader
    {
        private readonly DiContainer _container;

        private AsyncOperationHandle<SceneInstance> _currentScene;

        public SceneLoader(DiContainer container) => _container = container;

        public async UniTask LoadAsync(string sceneKey, CancellationToken token)
        {
            await UnloadCurrent(token);

            if (!await AddressablesAssetProvider.HasLocations(sceneKey, typeof(SceneInstance), token))
            {
                Debug.LogError($"[SceneLoader] Сцена '{sceneKey}' не найдена в Addressables.");
                return;
            }

            SceneContext.ParentContainers = new[] { _container };
            try
            {
                _currentScene = Addressables.LoadSceneAsync(sceneKey, LoadSceneMode.Additive);
                var instance = await _currentScene.ToUniTask(cancellationToken: token);
                SceneManager.SetActiveScene(instance.Scene);
            }
            finally
            {
                SceneContext.ParentContainers = null;
            }
        }

        private async UniTask UnloadCurrent(CancellationToken token)
        {
            if (!_currentScene.IsValid())
                return;

            await Addressables.UnloadSceneAsync(_currentScene).ToUniTask(cancellationToken: token);
            _currentScene = default;
        }
    }
}
