using System.Threading;
using AquariumGame.Infrastructure.Services;
using AquariumGame.Infrastructure.StateMachine;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Infrastructure.States
{
    /// <summary>
    /// 5. Аквариум: грузит игровую сцену и ждёт выхода в меню.
    /// </summary>
    public sealed class GameplayState : StateBase
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFlowService _gameFlow;

        public GameplayState(ISceneLoader sceneLoader, IGameFlowService gameFlow)
        {
            _sceneLoader = sceneLoader;
            _gameFlow = gameFlow;
        }

        public override UniTask Enter(CancellationToken token) =>
            _sceneLoader.LoadAsync(AssetKeys.GameplayScene, token);

        public override async UniTask<StateTransition> Execute(CancellationToken token)
        {
            await _gameFlow.WaitForMainMenuAsync(token);
            return Transition.GoTo<MainMenuState>();
        }
    }
}
