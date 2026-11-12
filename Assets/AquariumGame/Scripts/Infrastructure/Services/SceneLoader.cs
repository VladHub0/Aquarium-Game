using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace AquariumGame.Infrastructure.Services
{
    /// <summary>
    /// Загружает сцены аддитивно, чтобы Bootstrap-сцена (и её SceneContext) оставалась жива.
    /// Сначала ищет сцену в Addressables, иначе берёт её из Build Settings.
    /// </summary>
    public sealed class SceneLoader : ISceneLoader
    {
        private AsyncOperationHandle<SceneInstance> _addressableScene;
        private string _buildSettingsScene;

        public async UniTask LoadAsync(string sceneKey, CancellationToken token)
        {
            await UnloadCurrent(token);

            if (await AddressablesAssetProvider.HasLocations(sceneKey, typeof(SceneInstance), token))
            {
                _addressableScene = Addressables.LoadSceneAsync(sceneKey, LoadSceneMode.Additive);
                var instance = await _addressableScene.ToUniTask(cancellationToken: token);
                SceneManager.SetActiveScene(instance.Scene);
                return;
            }

            if (Application.CanStreamedLevelBeLoaded(sceneKey))
            {
                await SceneManager.LoadSceneAsync(sceneKey, LoadSceneMode.Additive)
                    .ToUniTask(cancellationToken: token);
                _buildSettingsScene = sceneKey;
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneKey));
                return;
            }

            Debug.LogError($"[SceneLoader] Сцена '{sceneKey}' не найдена ни в Addressables, ни в Build Settings.");
        }

        private async UniTask UnloadCurrent(CancellationToken token)
        {
            if (_addressableScene.IsValid())
            {
                await Addressables.UnloadSceneAsync(_addressableScene).ToUniTask(cancellationToken: token);
                _addressableScene = default;
            }

            if (!string.IsNullOrEmpty(_buildSettingsScene))
            {
                await SceneManager.UnloadSceneAsync(_buildSettingsScene).ToUniTask(cancellationToken: token);
                _buildSettingsScene = null;
            }
        }
    }
}
