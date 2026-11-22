using System.Threading;
using AquariumGame.Features.Fish.Scripts.Services.Spawner.Interface;
using AquariumGame.Infrastructure.Services;
using AquariumGame.Infrastructure.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AquariumGame.Infrastructure.States
{
    /// <summary>
    /// 5. Аквариум: грузит игровую сцену и ждёт выхода в меню.
    /// </summary>
    public sealed class GameplayState : StateBase
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFlowService _gameFlow;
        private readonly IFishSpawner _fishSpawner;

        public GameplayState(ISceneLoader sceneLoader, IGameFlowService gameFlow, IFishSpawner fishSpawner)
        {
            _sceneLoader = sceneLoader;
            _gameFlow = gameFlow;
            _fishSpawner = fishSpawner;
        }

        public override async UniTask Enter(CancellationToken token)
        {
            await _sceneLoader.LoadAsync(AssetKeys.GameplayScene, token);
            await _fishSpawner.SpawnAsync(Vector3.zero, token);
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
