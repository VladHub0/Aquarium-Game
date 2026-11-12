using System.Threading;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Infrastructure.Services
{
    /// <summary>
    /// Сигналы от UI к глобальной state machine.
    /// Например, кнопка Play вызывает RequestStartGame(), а MainMenuState ждёт этого сигнала.
    /// </summary>
    public interface IGameFlowService
    {
        void RequestStartGame();
        void RequestMainMenu();
        UniTask WaitForStartGameAsync(CancellationToken token);
        UniTask WaitForMainMenuAsync(CancellationToken token);
    }

    public sealed class GameFlowService : IGameFlowService
    {
        private UniTaskCompletionSource _startGame = new();
        private UniTaskCompletionSource _mainMenu = new();

        public void RequestStartGame() => _startGame.TrySetResult();
        public void RequestMainMenu() => _mainMenu.TrySetResult();

        public async UniTask WaitForStartGameAsync(CancellationToken token)
        {
            _startGame = new UniTaskCompletionSource();
            await _startGame.Task.AttachExternalCancellation(token);
        }

        public async UniTask WaitForMainMenuAsync(CancellationToken token)
        {
            _mainMenu = new UniTaskCompletionSource();
            await _mainMenu.Task.AttachExternalCancellation(token);
        }
    }
}
