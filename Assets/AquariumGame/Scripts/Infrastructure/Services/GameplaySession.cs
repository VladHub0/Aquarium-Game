using System.Threading;
using AquariumGame.Features.Fish.Scripts.Services.Spawner.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Waypoints.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AquariumGame.Infrastructure.Services
{
    public sealed class GameplaySession : IGameplaySession
    {
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

            _waypoints.TryGetRandomPoint(Vector3.zero, out var spawnPoint);
            await _fishSpawner.SpawnAsync(spawnPoint, token);

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
