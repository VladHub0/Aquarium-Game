using System.Threading;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Features.StateMachine.Scripts.GameFlow.Interface
{
    public interface IGameFlowService
    {
        void RequestStartGame();
        void RequestMainMenu();
        void RequestTogglePause();
        void RequestGameOver();
        void RequestVictory();
        UniTask WaitForStartGameAsync(CancellationToken token);
        UniTask WaitForMainMenuAsync(CancellationToken token);
        UniTask WaitForTogglePauseAsync(CancellationToken token);
        UniTask WaitForGameOverAsync(CancellationToken token);
        UniTask WaitForVictoryAsync(CancellationToken token);
    }
}
