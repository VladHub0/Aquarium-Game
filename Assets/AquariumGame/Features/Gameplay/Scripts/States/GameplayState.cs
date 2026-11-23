using System.Threading;
using AquariumGame.Features.Gameplay.Scripts.Services.Interface;
using AquariumGame.Features.MainMenu.Scripts.States;
using AquariumGame.Features.Pause.Scripts.States;
using AquariumGame.Features.StateMachine.Scripts.Core;
using AquariumGame.Features.StateMachine.Scripts.GameFlow.Interface;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Features.Gameplay.Scripts.States
{
    public sealed class GameplayState : StateBase
    {
        private readonly IGameplaySession _session;
        private readonly IGameFlowService _gameFlow;

        private bool _goingToPause;

        public GameplayState(IGameplaySession session, IGameFlowService gameFlow)
        {
            _session = session;
            _gameFlow = gameFlow;
        }

        public override UniTask Enter(CancellationToken token) => _session.StartAsync(token);

        public override async UniTask<StateTransition> Execute(CancellationToken token)
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(token);

            var index = await UniTask.WhenAny(
                _gameFlow.WaitForTogglePauseAsync(cts.Token),
                _gameFlow.WaitForMainMenuAsync(cts.Token));

            cts.Cancel();

            if (index == 0)
            {
                _goingToPause = true;
                return Transition.GoTo<PauseState>();
            }

            return Transition.GoTo<MainMenuState>();
        }

        public override UniTask Exit(CancellationToken token)
        {
            if (!_goingToPause)
                _session.End();

            return UniTask.CompletedTask;
        }
    }
}
