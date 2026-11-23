using System.Threading;
using AquariumGame.Features.Gameplay.Scripts.Services.Interface;
using AquariumGame.Features.MainMenu.Scripts.States;
using AquariumGame.Features.StateMachine.Scripts.Core;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Features.Victory.Scripts.States
{
    public sealed class VictoryState : StateBase
    {
        private readonly IGameplaySession _session;

        public VictoryState(IGameplaySession session) => _session = session;

        public override UniTask Enter(CancellationToken token)
        {
            _session.End();
            return UniTask.CompletedTask;
        }

        public override UniTask<StateTransition> Execute(CancellationToken token) =>
            UniTask.FromResult(Transition.GoTo<MainMenuState>());
    }
}
