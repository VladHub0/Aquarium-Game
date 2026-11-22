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

        private readonly ISceneLoader _sceneLoader;
        private readonly IFishSpawner _fishSpawner;
        private readonly IFishWaypointsService _waypoints;

        public bool IsActive { get; private set; }

        public GameplaySession(ISceneLoader sceneLoader, IFishSpawner fishSpawner, IFishWaypointsService waypoints)
        {
            _sceneLoader = sceneLoader;
            _fishSpawner = fishSpawner;
            _waypoints = waypoints;
        }

        public async UniTask StartAsync(CancellationToken token)
        {
            if (IsActive)
                return;

            await _sceneLoader.LoadAsync(AssetKeys.GameplayScene, token);

            var species = AssetKeys.FishSpecies;
            var spawnPoint = Vector3.zero;

            for (var i = 0; i < FishCount; i++)
            {
                _waypoints.TryGetRandomPoint(spawnPoint, out spawnPoint);

                var offset = Random.insideUnitCircle * SpawnSpread;
                var position = new Vector3(spawnPoint.x + offset.x, spawnPoint.y + offset.y, i * DepthStep);

                await _fishSpawner.SpawnAsync(species[i % species.Length], position, token);
            }

            IsActive = true;
        }

        public void End()
        {
            if (!IsActive)
                return;

            _fishSpawner.DespawnAll();
            IsActive = false;
        }
    }
}
