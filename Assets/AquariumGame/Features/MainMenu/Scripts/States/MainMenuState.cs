using System.Threading;
using AquariumGame.Features.AssetManagement.Scripts.Keys;
using AquariumGame.Features.Gameplay.Scripts.States;
using AquariumGame.Features.SceneLoading.Scripts.Services.Interface;
using AquariumGame.Features.StateMachine.Scripts.Core;
using AquariumGame.Features.StateMachine.Scripts.GameFlow.Interface;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Features.MainMenu.Scripts.States
{
    public sealed class MainMenuState : StateBase
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFlowService _gameFlow;

        public MainMenuState(ISceneLoader sceneLoader, IGameFlowService gameFlow)
        {
            _sceneLoader = sceneLoader;
            _gameFlow = gameFlow;
        }

        public override UniTask Enter(CancellationToken token) =>
            _sceneLoader.LoadAsync(AssetKeys.MainMenuScene, token);

        public override async UniTask<StateTransition> Execute(CancellationToken token)
        {
            await _gameFlow.WaitForStartGameAsync(token);
            return Transition.GoTo<GameplayState>();
        }
    }
}
