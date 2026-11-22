using System.Threading;
using AquariumGame.Features.Fish.Scripts.Services.Spawner.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Waypoints.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AquariumGame.Infrastructure.Services
{
    public sealed class GameplaySession : IGameplaySession
    {
        private const int FishCount = 5;
        private const float SpawnSpread = 0.3f;
        private const float DepthStep = 0.01f;
        private const int DepthLayers = 100;

        private readonly ISceneLoader _sceneLoader;
        private readonly IFishSpawner _fishSpawner;
        private readonly IFishWaypointsService _waypoints;
        private readonly IWalletService _wallet;

        private Vector3 _lastSpawnPoint;
        private int _spawnedCount;

        public bool IsActive { get; private set; }

        public GameplaySession(
            ISceneLoader sceneLoader,
            IFishSpawner fishSpawner,
            IFishWaypointsService waypoints,
            IWalletService wallet)
        {
            _sceneLoader = sceneLoader;
            _fishSpawner = fishSpawner;
            _waypoints = waypoints;
            _wallet = wallet;
        }

        public async UniTask StartAsync(CancellationToken token)
        {
            if (IsActive)
                return;

            _wallet.Reset();
            _spawnedCount = 0;
            _lastSpawnPoint = Vector3.zero;

            await _sceneLoader.LoadAsync(AssetKeys.GameplayScene, token);

            var species = FishCatalog.All;
            for (var i = 0; i < FishCount; i++)
                await SpawnAsync(species[i % species.Length].Key, token);

            IsActive = true;
        }

        public UniTask SpawnFishAsync(string speciesKey, CancellationToken token)
        {
            return IsActive ? SpawnAsync(speciesKey, token) : UniTask.CompletedTask;
        }

        public void End()
        {
            if (!IsActive)
                return;

            _fishSpawner.DespawnAll();
            IsActive = false;
        }

        private async UniTask SpawnAsync(string speciesKey, CancellationToken token)
        {
            _waypoints.TryGetRandomPoint(_lastSpawnPoint, out _lastSpawnPoint);

            var offset = Random.insideUnitCircle * SpawnSpread;
            var depth = (_spawnedCount % DepthLayers) * DepthStep;
            var position = new Vector3(_lastSpawnPoint.x + offset.x, _lastSpawnPoint.y + offset.y, depth);

            _spawnedCount++;
            await _fishSpawner.SpawnAsync(speciesKey, position, token);
        }
    }
}
