using System.Threading;
using AquariumGame.Features.AssetManagement.Scripts.Services;
using AquariumGame.Features.SceneLoading.Scripts.Services.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace AquariumGame.Features.SceneLoading.Scripts.Services
{
    public sealed class SceneLoader : ISceneLoader
    {
        private AsyncOperationHandle<SceneInstance> _currentScene;
        private AsyncOperationHandle<SceneInstance> _overlayScene;

        public async UniTask LoadAsync(string sceneKey, CancellationToken token)
        {
            await UnloadOverlayAsync(token);
            await Unload(_currentScene, token);
            _currentScene = default;

            _currentScene = await LoadAdditive(sceneKey, token);
            if (_currentScene.IsValid())
                SceneManager.SetActiveScene(_currentScene.Result.Scene);
        }

        public async UniTask LoadOverlayAsync(string sceneKey, CancellationToken token)
        {
            await UnloadOverlayAsync(token);
            _overlayScene = await LoadAdditive(sceneKey, token);
        }

        public async UniTask UnloadOverlayAsync(CancellationToken token)
        {
            await Unload(_overlayScene, token);
            _overlayScene = default;
        }

        private static async UniTask<AsyncOperationHandle<SceneInstance>> LoadAdditive(string sceneKey, CancellationToken token)
        {
            if (!await AddressablesAssetProvider.HasLocations(sceneKey, typeof(SceneInstance), token))
            {
                Debug.LogError($"[SceneLoader] Сцена '{sceneKey}' не найдена в Addressables.");
                return default;
            }

            var handle = Addressables.LoadSceneAsync(sceneKey, LoadSceneMode.Additive);
            await handle.ToUniTask(cancellationToken: token);
            return handle;
        }

        private static async UniTask Unload(AsyncOperationHandle<SceneInstance> handle, CancellationToken token)
        {
            if (!handle.IsValid())
                return;

            await Addressables.UnloadSceneAsync(handle).ToUniTask(cancellationToken: token);
        }
    }
}
