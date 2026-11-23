using System.Threading;
using AquariumGame.Features.AssetManagement.Scripts.Keys;
using AquariumGame.Features.Feeding.Scripts.Model.Interface;
using AquariumGame.Features.Feeding.Scripts.Services.Food.Interface;
using AquariumGame.Features.Fish.Scripts.Catalog;
using AquariumGame.Features.Fish.Scripts.Services.Spawner.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Waypoints.Interface;
using AquariumGame.Features.Gameplay.Scripts.Services.Interface;
using AquariumGame.Features.SceneLoading.Scripts.Services.Interface;
using AquariumGame.Features.Wallet.Scripts.Model.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AquariumGame.Features.Gameplay.Scripts.Services
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
        private readonly IWalletModel _wallet;
        private readonly IFoodService _food;
        private readonly IFeedingModeModel _feedingMode;

        private Vector3 _lastSpawnPoint;
        private int _spawnedCount;

        public bool IsActive { get; private set; }

        public GameplaySession(
            ISceneLoader sceneLoader,
            IFishSpawner fishSpawner,
            IFishWaypointsService waypoints,
            IWalletModel wallet,
            IFoodService food,
            IFeedingModeModel feedingMode)
        {
            _sceneLoader = sceneLoader;
            _fishSpawner = fishSpawner;
            _waypoints = waypoints;
            _wallet = wallet;
            _food = food;
            _feedingMode = feedingMode;
        }

        public async UniTask StartAsync(CancellationToken token)
        {
            if (IsActive)
                return;

            _wallet.Reset();
            _food.Clear();
            _feedingMode.SetActive(false);
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
            _food.Clear();
            _feedingMode.SetActive(false);
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
