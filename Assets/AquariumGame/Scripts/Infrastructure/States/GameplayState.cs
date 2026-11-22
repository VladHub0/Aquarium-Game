using System.Threading;
using AquariumGame.Features.Fish.Scripts.Services.Spawner.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Waypoints.Interface;
using AquariumGame.Infrastructure.Services;
using AquariumGame.Infrastructure.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AquariumGame.Infrastructure.States
{
    public sealed class GameplayState : StateBase
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFlowService _gameFlow;
        private readonly IFishSpawner _fishSpawner;
        private readonly IFishWaypointsService _waypoints;

        public GameplayState(
            ISceneLoader sceneLoader,
            IGameFlowService gameFlow,
            IFishSpawner fishSpawner,
            IFishWaypointsService waypoints)
        {
            _sceneLoader = sceneLoader;
            _gameFlow = gameFlow;
            _fishSpawner = fishSpawner;
            _waypoints = waypoints;
        }

        public override async UniTask Enter(CancellationToken token)
        {
            await _sceneLoader.LoadAsync(AssetKeys.GameplayScene, token);
            _waypoints.TryGetRandomPoint(Vector3.zero, out var spawnPoint);
            await _fishSpawner.SpawnAsync(spawnPoint, token);
        }

        public override async UniTask<StateTransition> Execute(CancellationToken token)
        {
            await _gameFlow.WaitForMainMenuAsync(token);
            return Transition.GoTo<MainMenuState>();
        }

        public override UniTask Exit(CancellationToken token)
        {
            _fishSpawner.DespawnAll();
            return UniTask.CompletedTask;
        }
    }
}
