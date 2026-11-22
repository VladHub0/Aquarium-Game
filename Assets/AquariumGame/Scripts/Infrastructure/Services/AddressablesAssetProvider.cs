using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace AquariumGame.Infrastructure.Services
{
    public sealed class AddressablesAssetProvider : IAssetProvider, IDisposable
    {
        private readonly Dictionary<string, AsyncOperationHandle> _cache = new();
        private readonly List<AsyncOperationHandle> _preloadHandles = new();

        public async UniTask InitializeAsync(CancellationToken token)
        {
            var handle = Addressables.InitializeAsync(false);
            try
            {
                await handle.ToUniTask(cancellationToken: token);
            }
            finally
            {
                if (handle.IsValid()) Addressables.Release(handle);
            }
        }

        public async UniTask UpdateCatalogsAsync(CancellationToken token)
        {
            var checkHandle = Addressables.CheckForCatalogUpdates(false);
            List<string> catalogs;
            try
            {
                catalogs = await checkHandle.ToUniTask(cancellationToken: token);
            }
            finally
            {
                if (checkHandle.IsValid()) Addressables.Release(checkHandle);
            }

            if (catalogs == null || catalogs.Count == 0)
                return;

            var updateHandle = Addressables.UpdateCatalogs(catalogs, false);
            try
            {
                await updateHandle.ToUniTask(cancellationToken: token);
            }
            finally
            {
                if (updateHandle.IsValid()) Addressables.Release(updateHandle);
            }
        }

        public async UniTask PreloadByLabelAsync(string label, CancellationToken token)
        {
            if (!await HasLocations(label, typeof(Object), token))
            {
                Debug.LogWarning($"[Assets] Нет ассетов с лейблом '{label}', предзагрузка пропущена.");
                return;
            }

            var handle = Addressables.LoadAssetsAsync<Object>((object)label, (Action<Object>)null);
            _preloadHandles.Add(handle);
            await handle.ToUniTask(cancellationToken: token);
        }

        public async UniTask<T> LoadAsync<T>(string key, CancellationToken token) where T : Object
        {
            if (_cache.TryGetValue(key, out var cached))
            {
                await cached.ToUniTask(cancellationToken: token);
                return cached.Result as T;
            }

            var handle = Addressables.LoadAssetAsync<T>(key);
            _cache[key] = handle;
            return await handle.ToUniTask(cancellationToken: token);
        }

        public void ReleaseAll()
        {
            foreach (var handle in _cache.Values)
                if (handle.IsValid()) Addressables.Release(handle);
            _cache.Clear();

            foreach (var handle in _preloadHandles)
                if (handle.IsValid()) Addressables.Release(handle);
            _preloadHandles.Clear();
        }

        public void Dispose() => ReleaseAll();

        internal static async UniTask<bool> HasLocations(object key, Type type, CancellationToken token)
        {
            var handle = Addressables.LoadResourceLocationsAsync(key, type);
            try
            {
                var locations = await handle.ToUniTask(cancellationToken: token);
                return locations != null && locations.Count > 0;
            }
            finally
            {
                if (handle.IsValid()) Addressables.Release(handle);
            }
        }
    }
}
