using System.Threading;
using AquariumGame.Infrastructure.Services;
using AquariumGame.Infrastructure.StateMachine;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Infrastructure.States
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
