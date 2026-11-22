using System;
using System.Collections.Generic;
using System.Threading;
using AquariumGame.Features.Fish.Scripts.Services.Facade.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Spawner.Interface;
using AquariumGame.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace AquariumGame.Features.Fish.Scripts.Services.Spawner
{
    public sealed class FishSpawner : IFishSpawner, IDisposable
    {
        private readonly DiContainer _container;
        private readonly IAssetProvider _assets;
        private readonly List<GameObject> _spawned = new();

        public FishSpawner(DiContainer container, IAssetProvider assets)
        {
            _container = container;
            _assets = assets;
        }

        public async UniTask<IFishFacade> SpawnAsync(string key, Vector3 position, CancellationToken token)
        {
            var prefab = await _assets.LoadAsync<GameObject>(key, token);
            if (prefab == null)
                throw new InvalidOperationException($"[FishSpawner] Префаб рыбы '{key}' не загружен.");

            token.ThrowIfCancellationRequested();

            var instance = _container.InstantiatePrefab(prefab, position, Quaternion.identity, null);
            _spawned.Add(instance);

            var context = instance.GetComponent<GameObjectContext>();
            if (context == null)
                throw new InvalidOperationException($"[FishSpawner] На префабе '{prefab.name}' нет GameObjectContext.");

            var facade = context.Container.Resolve<IFishFacade>();
            facade.Init();
            return facade;
        }

        public void DespawnAll()
        {
            foreach (var fish in _spawned)
                if (fish != null)
                    Object.Destroy(fish);

            _spawned.Clear();
        }

        public void Dispose() => DespawnAll();
    }
}
