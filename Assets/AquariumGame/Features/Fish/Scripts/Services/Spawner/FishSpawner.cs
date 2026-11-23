using System;
using System.Collections.Generic;
using System.Threading;
using AquariumGame.Features.AssetManagement.Scripts.Services.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Facade.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Factory;
using AquariumGame.Features.Fish.Scripts.Services.Factory.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Registry.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Spawner.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AquariumGame.Features.Fish.Scripts.Services.Spawner
{
    public sealed class FishSpawner : IFishSpawner, IDisposable
    {
        private readonly IAssetProvider _assets;
        private readonly IFishFactory _factory;
        private readonly IFishRegistry _registry;
        private readonly List<FishInstance> _spawned = new List<FishInstance>();

        public FishSpawner(IAssetProvider assets, IFishFactory factory, IFishRegistry registry)
        {
            _assets = assets;
            _factory = factory;
            _registry = registry;
        }

        public async UniTask<IFishFacade> SpawnAsync(string key, Vector3 position, CancellationToken token)
        {
            var prefab = await _assets.LoadAsync<GameObject>(key, token);
            if (prefab == null)
                throw new InvalidOperationException($"[FishSpawner] Префаб рыбы '{key}' не загружен.");

            token.ThrowIfCancellationRequested();

            var fish = _factory.Create(prefab, position);
            _spawned.Add(fish);

            fish.Facade.Init();
            _registry.Add(fish.Facade);

            return fish.Facade;
        }

        public void DespawnAll()
        {
            foreach (var fish in _spawned)
            {
                _registry.Remove(fish.Facade);
                fish.Facade.Dispose();

                if (fish.Root != null)
                    Object.Destroy(fish.Root);
            }

            _spawned.Clear();
        }

        public void Dispose() => DespawnAll();
    }
}
